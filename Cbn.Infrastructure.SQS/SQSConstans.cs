namespace Cbn.Infrastructure.SQS
{
    public static class SQSConstans
    {
        public const int MaxDelay = 900;
        public const string TypeFullNameKey = "sqs-type";
        public const string ReceiveCountKey = "sqs-count";
        public const string ApproximateReceiveCountKey = "ApproximateReceiveCount";
        public const string VisibilityTimeoutKey = "VisibilityTimeout";
    }
}