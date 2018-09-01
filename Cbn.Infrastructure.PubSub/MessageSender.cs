using System;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.PubSub.Extensions;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.PubSub
{
    public class MessageSender : IMessageSender
    {
        private IGoogleMessagingConfig messagingConfig;
        private Lazy<Task<PublisherClient>> publisherLazy;

        public MessageSender(IGoogleMessagingConfig messagingConfig)
        {
            this.messagingConfig = messagingConfig;
            this.publisherLazy = new Lazy<Task<PublisherClient>>(() => this.messagingConfig.CreatePublisherClientAsync());
        }

        public async Task<string> SendAsync<T>(T message) where T : class
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