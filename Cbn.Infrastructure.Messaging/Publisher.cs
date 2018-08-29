using System;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.Messaging.Extensions;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.Messaging
{
    public class Publisher : IPublisher
    {
        private IGoogleMessagingConfig messagingConfig;
        private Lazy<Task<PublisherClient>> publisherLazy;

        public Publisher(IGoogleMessagingConfig messagingConfig)
        {
            this.messagingConfig = messagingConfig;
            this.publisherLazy = new Lazy<Task<PublisherClient>>(() => this.messagingConfig.CreatePublisherClientAsync());
        }

        public async Task<string> PublishAsync<T>(T message) where T : class
        {
            var publisher = await this.publisherLazy.Value;
            var psMessage = new PubsubMessage();
            psMessage.Attributes.Add(nameof(Type), typeof(T).FullName);
            var text = JsonConvert.SerializeObject(message);
            psMessage.Data = ByteString.CopyFromUtf8(text);
            return await publisher.PublishAsync(psMessage);
        }
    }
}