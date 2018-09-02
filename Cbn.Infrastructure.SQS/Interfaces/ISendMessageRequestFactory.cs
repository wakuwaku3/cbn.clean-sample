using System;
using Amazon.SQS.Model;

namespace Cbn.Infrastructure.SQS.Interfaces
{
    public interface ISendMessageRequestFactory
    {
        SendMessageRequest CreateSendMessage(object message, Type type, int receiveCount = 0);
        SendMessageRequest CreateSendMessage<T>(T message, int receiveCount = 0) where T : class;
    }
}