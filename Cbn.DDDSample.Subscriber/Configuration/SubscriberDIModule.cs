using System.Net.Mail;
using System.Reflection;
using System.Threading;
using Cbn.DDDSample.Application;
using Cbn.DDDSample.Common;
using Cbn.DDDSample.Domain.Account;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Subscriber.Executers;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.DDDSampleData;
using Cbn.Infrastructure.JsonWebToken;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Cbn.Infrastructure.Messaging;
using Cbn.Infrastructure.Messaging.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Migration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.DDDSample.Subscriber.Configuration
{
    public class SubscriberDIModule : IDIModule
    {
        private Assembly executeAssembly;
        private string rootPath;
        private IConfigurationRoot configurationRoot;
        private ILoggerFactory loggerFactory;
        private CommonDIModule commonAutofacModule;

        public SubscriberDIModule(Assembly executeAssembly, string rootPath, IConfigurationRoot configurationRoot, ILoggerFactory loggerFactory)
        {
            this.executeAssembly = executeAssembly;
            this.rootPath = rootPath;
            this.configurationRoot = configurationRoot;
            this.loggerFactory = loggerFactory;
            this.commonAutofacModule = new CommonDIModule(executeAssembly, rootPath, loggerFactory);
        }

        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterModule(this.commonAutofacModule);
            builder.RegisterModule(new AutofacDIModule());
            builder.RegisterModule(new JwtDIModule());
            builder.RegisterModule(new DDDSampleDataDIModule(this.configurationRoot.GetConnectionString("DefaultConnection")));
            builder.RegisterModule(new DDDSampleCommonDIModule());
            builder.RegisterModule(new DomainAccountDIModule(LifetimeType.Singleton));
            builder.RegisterModule(new ApplicationDIModule());
            builder.RegisterModule(new MessagingDIModule());
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterModule(new MigrationDIModule(this.configurationRoot.GetConnectionString("MigrationConnection")));
            builder.RegisterType<DDDSampleConfig>(x =>
                x.As<IDbConfig>()
                .As<IJwtConfig>()
                .As<IMigrationConfig>()
                .As<IMessagingConfig>()
                .SingleInstance());

            builder.RegisterType<WelcomeMailSender>(x => x.As<IExecuter<WelcomeMailArgs>>());
            builder.RegisterType<CancellationTokenSource>(x => x.SingleInstance());
        }
    }
}