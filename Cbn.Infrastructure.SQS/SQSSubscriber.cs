using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.Common.ValueObjects;
using Cbn.Infrastructure.SQS.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.SQS
{
    public class SQSSubscriber : IMessageSubscriber, IDisposable
    {
        private ISQSConfig config;
        private ISystemClock clock;
        private ILogger logger;
        private ISQSClientProvider sqsClientProvider;
        private ISendMessageRequestFactory sendMessageRequestFactory;
        private CancellationTokenSource tokenSource;
        private ITypeHelper typeHelper;
        private IScopeProvider scopeProvider;
        private SemaphoreSlim semaphoreSlim;

        public SQSSubscriber(
            ISQSConfig config,
            ILogger logger,
            ISystemClock clock,
            ISQSClientProvider sqsClientProvider,
            ISendMessageRequestFactory sendMessageRequestFactory,
            CancellationTokenSource tokenSource,
            ITypeHelper typeHelper,
            IScopeProvider scopeProvider)
        {
            this.config = config;
            this.clock = clock;
            this.logger = logger;
            this.sqsClientProvider = sqsClientProvider;
            this.sendMessageRequestFactory = sendMessageRequestFactory;
            this.tokenSource = tokenSource;
            this.typeHelper = typeHelper;
            this.scopeProvider = scopeProvider;
            this.semaphoreSlim = config.MaxConcurrencyReceive > 0 ? new SemaphoreSlim(config.MaxConcurrencyReceive) : null;
        }

        private IAmazonSQS SQSClient => this.sqsClientProvider.SQSClient;

        public async Task SubscribeAsync()
        {
            var targets = this.config.SQSQueueSettings
                .Concat(new [] { this.config.DefaultSQSQueueSetting })
                .ToDictionary(x => x.QueueUrl)
                .Select(x => x.Value);
            var tasks = targets.SelectMany(setting =>
            {
                return Enumerable.Range(0, setting.InstanceCount).Select(i =>
                {
                    return this.SubscribeAsync(setting);
                });
            });

            await Task.WhenAll(tasks);
        }

        private async Task SubscribeAsync(SQSQueueSetting setting)
        {
            var queueInfo = await this.GetQueueInfoAsync(setting);
            while (!this.tokenSource.Token.IsCancellationRequested)
            {
                await this.semaphoreSlim?.WaitAsync();
                if (setting.DelayType != SQSDelayType.FirstTimeOnly)
                {
                    await this.ProcessMessageWithDelayAsync(setting, queueInfo);
                }
                else
                {
                    await this.ProcessMessageAsync(setting, queueInfo);
                }
                this.semaphoreSlim?.Release();
            }
        }

        private async Task ProcessMessageAsync(SQSQueueSetting setting, GetQueueAttributesResponse queueInfo)
        {
            var receiveMessageResponse = await this.ReceiveMessageAsync(setting);
            if ((receiveMessageResponse?.Messages?.Count ?? 0) == 0)
            {
                return;
            }
            var message = receiveMessageResponse.Messages.Single();
            try
            {
                using(var source = new CancellationTokenSource(queueInfo.VisibilityTimeout * 900))
                {
                    this.logger.LogInformation($"Receive {message.MessageId} at {DateTime.Now}");
                    var result = await this.ExecuteAsync(message, source);
                    if (result != 0)
                    {
                        this.logger.LogInformation($"Handling Error {message.MessageId} at {DateTime.Now}(code:{result})");
                        await this.NoticeFailureAsync(setting, message);
                        return;
                    }
                    await this.DeleteMessageAsync(setting, message);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    this.logger.LogError($"Time out {message.MessageId} at {DateTime.Now}");
                }
                else
                {
                    this.logger.LogError(ex, $"Error {message.MessageId} at {DateTime.Now}");
                }
                await this.NoticeFailureAsync(setting, message);
            }
        }

        private async Task DeleteMessageAsync(SQSQueueSetting setting, Message message, int count = 0)
        {
            try
            {
                var deleteMessageRequest = new DeleteMessageRequest(setting.QueueUrl, message.ReceiptHandle);
                await this.SQSClient.DeleteMessageAsync(deleteMessageRequest);
                this.logger.LogInformation($"Delete message (id:{message.MessageId})");
            }
            catch (Exception ex)
            {
                await this.DeleteMessageAsync(setting, message, count++);
                if (count >= 10)
                {
                    this.logger.LogCritical(ex, $"Critical Error Can't delete message (id:{message.MessageId})");
                    throw;
                }
            }
        }

        private async Task NoticeFailureAsync(SQSQueueSetting setting, Message message, int count = 0)
        {
            try
            {
                var changeRequest = new ChangeMessageVisibilityRequest(setting.QueueUrl, message.ReceiptHandle, 0);
                await this.SQSClient.ChangeMessageVisibilityAsync(changeRequest);
                this.logger.LogInformation($"Change visibility message (id:{message.MessageId})");
            }
            catch (Exception ex)
            {
                await this.NoticeFailureAsync(setting, message, count++);
                if (count >= 10)
                {
                    this.logger.LogCritical(ex, $"Critical Error Can't change visibility message (id:{message.MessageId})");
                    throw;
                }
            }
        }

        private async Task<int> ExecuteAsync(Message message, CancellationTokenSource source)
        {
            var type = this.typeHelper.GetType(x => x.FullName == (message.MessageAttributes[SQSConstans.TypeFullNameKey].StringValue));
            var obj = JsonConvert.DeserializeObject(message.Body, type);
            var executerType = typeof(IMessageReceiver<>).MakeGenericType(type);
            var inheritTokenSource = new TypeValuePair(source);
            using(var scope = this.scopeProvider.BeginLifetimeScope(inheritTokenSource))
            {
                var pair = new TypeValuePair(obj, type);
                var executer = scope.Resolve(executerType, pair) as IMessageReceiver;
                return await executer.ExecuteAsync();
            }
        }

        private async Task ProcessMessageWithDelayAsync(SQSQueueSetting setting, GetQueueAttributesResponse queueInfo)
        {
            var receiveMessageResponse = await this.ReceiveMessageAsync(setting);
            if ((receiveMessageResponse?.Messages?.Count ?? 0) == 0)
            {
                return;
            }
            var message = receiveMessageResponse.Messages.Single();
            try
            {
                using(var source = new CancellationTokenSource(queueInfo.VisibilityTimeout * 900))
                {
                    this.logger.LogInformation($"Receive {message.MessageId} at {DateTime.Now}");
                    var result = await this.ExecuteAsync(message, source);
                    if (result != 0)
                    {
                        this.logger.LogInformation($"Handling Error {message.MessageId} at {DateTime.Now}(code:{result})");
                        await this.ResendMessageAsync(setting, message);
                        return;
                    }
                    await this.DeleteMessageAsync(setting, message);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    this.logger.LogError($"Time out {message.MessageId} at {DateTime.Now}");
                }
                else
                {
                    this.logger.LogError(ex, $"Error {message.MessageId} at {DateTime.Now}");
                }
                await this.ResendMessageAsync(setting, message);
            }
        }
        private async Task ResendMessageAsync(SQSQueueSetting setting, Message message, int count = 0)
        {
            try
            {
                var type = this.typeHelper.GetType(x => x.FullName == (message.MessageAttributes[SQSConstans.TypeFullNameKey].StringValue));
                var obj = JsonConvert.DeserializeObject(message.Body, type);
                var receiveCount = int.Parse(message.MessageAttributes[SQSConstans.ReceiveCountKey].StringValue);
                var sendMessageRequest = this.sendMessageRequestFactory.CreateSendMessage(message, receiveCount + 1);
                var sendMessageResponse = await this.SQSClient.SendMessageAsync(sendMessageRequest);
                this.logger.LogInformation($"Resend message (id:{message.MessageId})");
            }
            catch (Exception ex)
            {
                await this.ResendMessageAsync(setting, message, count++);
                if (count >= 10)
                {
                    this.logger.LogCritical(ex, $"Critical Error Can't resend message (id:{message.MessageId})");
                    throw;
                }
            }
            await this.DeleteMessageAsync(setting, message);
        }

        private async Task<GetQueueAttributesResponse> GetQueueInfoAsync(SQSQueueSetting setting)
        {
            return await this.SQSClient.GetQueueAttributesAsync(setting.QueueUrl, new List<string> { SQSConstans.VisibilityTimeoutKey }, this.tokenSource.Token);
        }

        private async Task<ReceiveMessageResponse> ReceiveMessageAsync(SQSQueueSetting setting)
        {
            var receiveMessageRequest = new ReceiveMessageRequest(setting.QueueUrl)
            {
                AttributeNames = { SQSConstans.ApproximateReceiveCountKey },
                MessageAttributeNames = { SQSConstans.TypeFullNameKey, SQSConstans.ReceiveCountKey },
            };

            return await this.SQSClient.ReceiveMessageAsync(receiveMessageRequest);
        }

        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.semaphoreSlim?.Dispose();
                }
                this.disposedValue = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}