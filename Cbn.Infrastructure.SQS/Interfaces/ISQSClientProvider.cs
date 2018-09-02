using Amazon.SQS;

namespace Cbn.Infrastructure.SQS.Interfaces
{
    public interface ISQSClientProvider
    {
        IAmazonSQS SQSClient { get; }
    }
}