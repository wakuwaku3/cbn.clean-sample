using System.Reflection;
using Cbn.DDDSample.Application;
using Cbn.DDDSample.Common;
using Cbn.DDDSample.Common.Configuration;
using Cbn.DDDSample.Domain.Account;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.DDDSampleData;
using Cbn.Infrastructure.JsonWebToken;
using Cbn.Infrastructure.Messaging;
using Cbn.Infrastructure.Npgsql.Entity.Migration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.DDDSample.Cli.Configuration
{
    public class CliDIModule : IDIModule
    {
        private Assembly executeAssembly;
        private string rootPath;
        private IConfigurationRoot configurationRoot;
        private ILoggerFactory loggerFactory;
        private CommonDIModule commonAutofacModule;

        public CliDIModule(Assembly executeAssembly, string rootPath, IConfigurationRoot configurationRoot, ILoggerFactory loggerFactory)
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
            builder.RegisterModule(new MessagingDIModule());
            builder.RegisterModule(new DDDSampleCommonDIModule(LifetimeType.Singleton));
            builder.RegisterModule(new DomainAccountDIModule());
            builder.RegisterModule(new ApplicationDIModule());
            builder.RegisterModule(new MigrationDIModule(this.configurationRoot.GetConnectionString("MigrationConnection")));
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterType<DDDSampleConfig>(x =>
                x.As<IDbConfig>()
                .As<IJwtConfig>()
                .As<IMigrationConfig>()
                .As<IGoogleMessagingConfig>()
                .SingleInstance());
        }
    }
}