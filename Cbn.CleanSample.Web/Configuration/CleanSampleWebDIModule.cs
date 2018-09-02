using Cbn.CleanSample.Domain.Account;
using Cbn.CleanSample.Domain.Common;
using Cbn.CleanSample.UseCases;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.CleanSampleData;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.Foundation;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.JsonWebToken;
using Cbn.Infrastructure.Npgsql.Entity.Migration;
using Cbn.Infrastructure.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.CleanSample.Web.Configuration
{
    public class CleanSampleWebDIModule : IDIModule
    {
        private System.Reflection.Assembly executeAssembly;
        private string rootPath;
        private CommonDIModule commonAutofacModule;
        private IConfigurationRoot configurationRoot;

        public CleanSampleWebDIModule(
            System.Reflection.Assembly executeAssembly,
            string rootPath,
            IConfigurationRoot configurationRoot,
            ILoggerFactory loggerFactory)
        {
            this.executeAssembly = executeAssembly;
            this.rootPath = rootPath;
            this.commonAutofacModule = new CommonDIModule(executeAssembly, rootPath, loggerFactory);
            this.configurationRoot = configurationRoot;
        }

        public void DefineModule(IDIBuilder builder)
        {
            var mapRegister = new MapRegister();
            builder.RegisterInstance(mapRegister, x => x.As<IMapRegister>());
            builder.RegisterModule(this.commonAutofacModule);
            builder.RegisterModule(new AutofacDIModule());
            builder.RegisterModule(new JwtDIModule());
            builder.RegisterModule(new CleanSampleDataDIModule(this.configurationRoot.GetConnectionString("DefaultConnection")));
            builder.RegisterModule(new SQSDIModule());
            builder.RegisterModule(new CleanSampleDomainCommonDIModule(LifetimeType.Scoped));
            builder.RegisterModule(new DomainAccountDIModule());
            builder.RegisterModule(new UseCasesDIModule());
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterModule(new MigrationDIModule(this.configurationRoot.GetConnectionString("MigrationConnection")));
            builder.RegisterType<CleanSampleWebConfig>(x =>
                x.As<IDbConfig>()
                .As<IWebConfig>()
                .As<IJwtConfig>()
                .As<IMigrationConfig>()
                .As<IPubSubConfig>()
                .SingleInstance());
        }
    }
}