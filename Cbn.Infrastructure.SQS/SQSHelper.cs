using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SQS.Model;
using Cbn.Infrastructure.Common.Messaging;
using Cbn.Infrastructure.SQS.Interfaces;

namespace Cbn.Infrastructure.SQS
{
    public class SQSHelper : ISQSHelper
    {
        public int ComputeDelaySeconds(SQSDelayType type, int delay, int count)
        {
            if (count == 0)
            {
                return 0;
            }
            switch (type)
            {
                case SQSDelayType.Constant:
                    return delay;
                case SQSDelayType.LinerIncrease:
                    {
                        var tmp = delay * (count - 1);
                        return tmp > SQSConstans.MaxDelay ? SQSConstans.MaxDelay : tmp;
                    }
                case SQSDelayType.ExponentialIncrease:
                    {
                        var tmp = delay * Math.Exp(count - 1);
                        return tmp > SQSConstans.MaxDelay ? SQSConstans.MaxDelay : (int) Math.Truncate(tmp);
                    }
            }
            return 0;
        }

        public Dictionary<string, MessageAttributeValue> CreateMessageAttributes(Type type, int receiveCount)
        {
            return Create().ToDictionary(x => x.key, x => x.value);
            IEnumerable < (string key, MessageAttributeValue value) > Create()
            {
                yield return (SQSConstans.TypeFullNameKey, CreateMessageAttributeValue(type.FullName));
                yield return (SQSConstans.ReceiveCountKey, CreateMessageAttributeValue(receiveCount.ToString()));
            }
            MessageAttributeValue CreateMessageAttributeValue(string v)
            {
                return new MessageAttributeValue()
                {
                    DataType = nameof(String),
                        StringValue = v,
                };
            }
        }
    }
}