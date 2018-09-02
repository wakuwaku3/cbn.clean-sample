using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cbn.Infrastructure.Common.Foundation.Exceptions;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.SQS.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.SQS
{
    public class SQSMessageSender : IMessageSender
    {
        private ISQSConfig config;
        private ISystemClock clock;
        private ILogger logger;
        private ISQSClientProvider sqsClientProvider;
        private ISendMessageRequestFactory sendMessageRequestFactory;

        public SQSMessageSender(
            ISQSConfig config,
            ILogger logger,
            ISystemClock clock,
            ISQSClientProvider sqsClientProvider,
            ISendMessageRequestFactory sendMessageRequestFactory)
        {
            this.config = config;
            this.clock = clock;
            this.logger = logger;
            this.sqsClientProvider = sqsClientProvider;
            this.sendMessageRequestFactory = sendMessageRequestFactory;
        }

        private IAmazonSQS SQSClient => this.sqsClientProvider.SQSClient;

        public async Task<string> SendAsync<T>(T message) where T : class
        {
            var sendMessageRequest = this.sendMessageRequestFactory.CreateSendMessage(message);
            var sendMessageResponse = await this.SQSClient.SendMessageAsync(sendMessageRequest);
            this.logger.LogInformation($"Send {sendMessageResponse.MessageId} at {DateTime.Now}:{typeof(T).FullName}");
            return sendMessageResponse.MessageId;
        }
    }

}