namespace Cbn.Infrastructure.Messaging.Interfaces
{
    public interface IMessagingConfig
    {
        string ProjectId { get; }
        string TopicId { get; }
        string SubscriptionId { get; }
    }
}