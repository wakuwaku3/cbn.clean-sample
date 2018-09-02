using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.SQS.Interfaces;

namespace Cbn.Infrastructure.SQS
{
    public class SQSDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<SendMessageRequestFactory>(x => x.As<ISendMessageRequestFactory>().SingleInstance());
            builder.RegisterType<SQSClientProvider>(x => x.As<ISQSClientProvider>().SingleInstance());
            builder.RegisterType<SQSHelper>(x => x.As<ISQSHelper>().SingleInstance());
            builder.RegisterType<SQSMessageSender>(x => x.As<IMessageSender>().SingleInstance());
            builder.RegisterType<SQSSubscriber>(x => x.As<IMessageSubscriber>().SingleInstance());
        }
    }
}