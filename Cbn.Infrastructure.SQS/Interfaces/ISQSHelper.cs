using System;
using System.Collections.Generic;
using Amazon.SQS.Model;
using Cbn.Infrastructure.Common.Messaging;

namespace Cbn.Infrastructure.SQS.Interfaces
{
    public interface ISQSHelper
    {
        int ComputeDelaySeconds(SQSDelayType type, int delay, int count);
        Dictionary<string, MessageAttributeValue> CreateMessageAttributes(Type type, int receiveCount);
    }
}