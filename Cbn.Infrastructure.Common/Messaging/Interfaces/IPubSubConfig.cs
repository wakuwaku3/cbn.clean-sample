namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IPubSubConfig
    {
        string ProjectId { get; }
        string TopicId { get; }
        string SubscriptionId { get; }
    }
}