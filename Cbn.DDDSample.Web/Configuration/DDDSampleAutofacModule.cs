using System;
using Autofac;
using Cbn.DDDSample.Application.Configuration;
using Cbn.DDDSample.Domain.Configuration;
using Cbn.DDDSample.Web.Configuration.Interfaces;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.DDDSample.Web.Configuration
{
    public class DDDSampleAutofacModule : Module
    {
        private System.Reflection.Assembly executeAssembly;
        private string rootPath;
        private CommonAutofacModule commonAutofacModule;
        private IConfigurationRoot configurationRoot;

        public DDDSampleAutofacModule(
            System.Reflection.Assembly executeAssembly,
            string rootPath,
            IConfigurationRoot configurationRoot,
            ILoggerFactory loggerFactory)
        {
            this.executeAssembly = executeAssembly;
            this.rootPath = rootPath;
            this.commonAutofacModule = new CommonAutofacModule(executeAssembly, rootPath, loggerFactory);
            this.configurationRoot = configurationRoot;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(this.commonAutofacModule);
            builder.RegisterModule(new DDDSampleDataAutofacModule());
            builder.RegisterModule(new DDDSampleDomainAutofacModule());
            builder.RegisterModule(new DDDSampleApplicationAutofacModule());
            builder.RegisterInstance(this.configurationRoot).As<IConfigurationRoot>().SingleInstance();
            builder.RegisterType<DDDSampleWebConfig>()
                .As<IDDDSampleWebConfig>()
                .As<IWebConfig>()
                .SingleInstance();
        }
    }
}