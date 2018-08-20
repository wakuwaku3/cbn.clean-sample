using Cbn.DDDSample.Application.Configuration;
using Cbn.DDDSample.Domain.Configuration;
using Cbn.DDDSample.Web.Configuration.Interfaces;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Configuration;
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
            builder.RegisterModule(new DDDSampleDataDIModule());
            builder.RegisterModule(new DDDSampleDomainDIModule());
            builder.RegisterModule(new DDDSampleApplicationDIModule());
            builder.RegisterInstance(this.configurationRoot, x => x.As<IConfigurationRoot>());
            builder.RegisterType<DDDSampleWebConfig>(x => x.As<IDDDSampleWebConfig>().As<IWebConfig>().SingleInstance());
        }
    }
}