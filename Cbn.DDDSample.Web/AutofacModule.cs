using System;
using Autofac;
using Cbn.Infrastructure.Autofac;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;

namespace Cbn.DDDSample.Web
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
            builder.RegisterInstance(new AutofacScopeProvider())
                .As<IScopeProvider>()
                .SingleInstance();
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IScopeProvider>().CurrentScope = new AutofacScopeWrapper(container);
            });
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