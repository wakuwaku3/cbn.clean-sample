using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Google.Cloud.PubSub.V1;

namespace Cbn.Infrastructure.PubSub.Extensions
{
    public static class MessagingExtensions
    {
        public static TopicName CreateTopicName(this IPubSubConfig messagingConfig)
        {
            return new TopicName(messagingConfig.ProjectId, messagingConfig.TopicId);
        }
        public static async Task<PublisherClient> CreatePublisherClientAsync(this IPubSubConfig messagingConfig)
        {
            var publisherServiceApiClient = await PublisherServiceApiClient.CreateAsync();
            return PublisherClient.Create(messagingConfig.CreateTopicName(), new [] { publisherServiceApiClient });
        }
        public static SubscriptionName CreateSubscriptionName(this IPubSubConfig messagingConfig)
        {
            return new SubscriptionName(messagingConfig.ProjectId, messagingConfig.SubscriptionId);
        }
        public static async Task<SubscriberClient> CreateSubscriberClientAsync(this IPubSubConfig messagingConfig)
        {
            var subscriberServiceApiClient = await SubscriberServiceApiClient.CreateAsync();
            return SubscriberClient.Create(messagingConfig.CreateSubscriptionName(), new [] { subscriberServiceApiClient });
        }
    }
}