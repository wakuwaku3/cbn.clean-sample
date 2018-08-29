namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IGoogleMessagingConfig
    {
        string ProjectId { get; }
        string TopicId { get; }
        string SubscriptionId { get; }
    }
}