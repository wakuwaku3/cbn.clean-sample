using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.Infrastructure.Messaging
{
    public class MessagingDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<Publisher>(x => x.As<IPublisher>().SingleInstance());
            builder.RegisterType<Subscriber>(x => x.As<ISubscriber>().SingleInstance());
        }
    }
}