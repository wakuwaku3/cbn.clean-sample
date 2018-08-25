using Cbn.DDDSample.Application;
using Cbn.DDDSample.Common;
using Cbn.DDDSample.Domain;
using Cbn.DDDSample.Domain.Account;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData;
using Cbn.Infrastructure.JsonWebToken;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.DDDSample.Web.Configuration
{
    public class DDDSampleWebDIModule : IDIModule
    {
        private System.Reflection.Assembly executeAssembly;
        private string rootPath;
        private CommonDIModule commonAutofacModule;
        private IConfigurationRoot configurationRoot;

        public DDDSampleWebDIModule(
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
            builder.RegisterModule(this.commonAutofacModule);
            builder.RegisterModule(new AutofacDIModule());
            builder.RegisterModule(new JwtDIModule());
            builder.RegisterModule(new DDDSampleDataDIModule(this.configurationRoot.GetConnectionString("DefaultConnection")));
            builder.RegisterModule(new DDDSampleCommonDIModule());
            builder.RegisterModule(new DomainAccountDIModule(LifetimeType.Scoped));
            builder.RegisterModule(new ApplicationDIModule());
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterType<DDDSampleWebConfig>(x =>
                x.As<IDbConfig>()
                .As<IWebConfig>()
                .As<IJwtConfig>()
                .SingleInstance());
        }
    }
}