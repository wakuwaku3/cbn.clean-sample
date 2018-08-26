using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.Common.ValueObjects;
using Cbn.Infrastructure.Messaging.Extensions;
using Cbn.Infrastructure.Messaging.Interfaces;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cbn.Infrastructure.Messaging
{
    public class Subscriber : ISubscriber
    {
        private ILogger logger;
        private IMessagingConfig messagingConfig;
        private IScopeProvider scopeProvider;
        private CancellationTokenSource tokenSource;
        private ITypeHelper typeHelper;
        private Lazy<Task<SubscriberClient>> subscriberLazy;

        public Subscriber(
            IMessagingConfig messagingConfig,
            IScopeProvider scopeProvider,
            ILogger logger,
            CancellationTokenSource tokenSource,
            ITypeHelper typeHelper)
        {
            this.logger = logger;
            this.messagingConfig = messagingConfig;
            this.scopeProvider = scopeProvider;
            this.tokenSource = tokenSource;
            this.typeHelper = typeHelper;
            this.subscriberLazy = new Lazy<Task<SubscriberClient>>(() => this.messagingConfig.CreateSubscriberClientAsync());
        }
        public async Task SubscribeAsync()
        {
            var subscriber = await this.subscriberLazy.Value;
            this.tokenSource.Token.Register(() =>
            {
                subscriber.StopAsync(CancellationToken.None).Wait();
            });
            await subscriber.StartAsync(async(PubsubMessage message, CancellationToken token) =>
            {
                var text = Encoding.UTF8.GetString(message.Data.ToArray());
                var type = this.typeHelper.GetType(x => x.FullName == (message.Attributes[nameof(Type)]));
                var executerType = typeof(IExecuter<>).MakeGenericType(type);
                try
                {
                    var obj = JsonConvert.DeserializeObject(text, type);
                    var pair = new TypeValuePair(obj, type);
                    using(var scope = this.scopeProvider.BeginLifetimeScope())
                    {
                        var executer = scope.Resolve(executerType, pair) as IExecuter;
                        var res = await executer.ExecuteAsync();
                        return res == 0 ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack;
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"Error {type} {executerType} {text}");
                    return SubscriberClient.Reply.Nack;
                }
            });
        }
    }
}