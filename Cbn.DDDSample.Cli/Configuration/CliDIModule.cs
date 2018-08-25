using System.Reflection;
using Cbn.DDDSample.Application;
using Cbn.DDDSample.Common;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData;
using Cbn.Infrastructure.JsonWebToken;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
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
            builder.RegisterModule(new DDDSampleDataDIModule());
            builder.RegisterModule(new DDDSampleCommonDIModule());
            builder.RegisterModule(new ApplicationDIModule());
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterType<CliConfig>(x =>
                x.As<IDbConfig>()
                .As<IJwtConfig>()
                .SingleInstance());
        }
    }
}