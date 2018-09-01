using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.Infrastructure.PubSub
{
    public class MessagingDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<MessageSender>(x => x.As<IMessageSender>().SingleInstance());
            builder.RegisterType<Subscriber>(x => x.As<IMessageSubscriber>().SingleInstance());
        }
    }
}