using System;
using Autofac;
using Cbn.DDDSample.Web.Configuration.Interfaces;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Autofac.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
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
            builder.RegisterInstance(this.configurationRoot).As<IConfigurationRoot>().SingleInstance();
            builder.RegisterType<DDDSampleWebConfig>()
                .As<IDDDSampleWebConfig>()
                .As<IWebConfig>()
                .SingleInstance();
            builder.RegisterType<Service>()
                .As<IService1>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Service>()
                .As<IService2>()
                .InstancePerMatchingLifetimeScope("scope1");
            builder.RegisterType<Service>()
                .As<IService3>()
                .InstancePerMatchingLifetimeScope("scope2");
            builder.RegisterType<Service>()
                .As<IService4>()
                .InstancePerDependency();
            builder.RegisterType<Service>()
                .As<IService5>()
                .SingleInstance();
        }
    }
    interface IService1
    {
        Guid Guid { get; }
    }
    interface IService2 : IService1 { }
    interface IService3 : IService2 { }
    interface IService4 : IService3 { }
    interface IService5 : IService4 { }
    class Service : IService5
    {
        public Guid Guid { get; } = Guid.NewGuid();
    }
}