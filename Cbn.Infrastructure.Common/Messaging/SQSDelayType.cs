namespace Cbn.Infrastructure.Common.Messaging
{
    public enum SQSDelayType
    {
        FirstTimeOnly,
        Constant,
        LinerIncrease,
        ExponentialIncrease,
    }
}