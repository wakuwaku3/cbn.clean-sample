using System;
using Amazon.SQS.Model;
using Cbn.Infrastructure.Common.Foundation.Exceptions;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.SQS.Interfaces;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.SQS
{
    public class SendMessageRequestFactory : ISendMessageRequestFactory
    {
        private ISQSConfig config;
        private ISQSHelper sqsHelper;

        public SendMessageRequestFactory(ISQSConfig config, ISQSHelper sqsHelper)
        {
            this.config = config;
            this.sqsHelper = sqsHelper;
        }
        public SendMessageRequest CreateSendMessage(object message, Type type, int receiveCount = 0)
        {
            if (!config.SQSQueueSettingDictionary.TryGetValue(type.Name, out var queueSetting))
            {
                queueSetting = config.DefaultSQSQueueSetting;
            }
            var json = JsonConvert.SerializeObject(message);
            var sendMessageRequest = new SendMessageRequest(queueSetting.QueueUrl, json)
            {
                DelaySeconds = this.sqsHelper.ComputeDelaySeconds(queueSetting.DelayType, queueSetting.FirstDelaySeconds, receiveCount),
                MessageAttributes = this.sqsHelper.CreateMessageAttributes(type, receiveCount),
            };
            return sendMessageRequest;
        }
        public SendMessageRequest CreateSendMessage<T>(T message, int receiveCount = 0) where T : class
        {
            return this.CreateSendMessage(message, typeof(T), receiveCount);
        }
    }
}