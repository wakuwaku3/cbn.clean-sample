using Autofac;
using Cbn.DDDSample.Domain.Home;
using Cbn.Infrastructure.DDDSampleData.Configuration;

namespace Cbn.DDDSample.Domain.Configuration
{
    public class DDDSampleDomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Query>()
                .As<IQuery1>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Query>()
                .As<IQuery2>()
                .InstancePerMatchingLifetimeScope("scope1");
            builder.RegisterType<Query>()
                .As<IQuery3>()
                .InstancePerMatchingLifetimeScope("scope2");
            builder.RegisterType<Query>()
                .As<IQuery4>()
                .InstancePerDependency();
            builder.RegisterType<Query>()
                .As<IQuery5>()
                .SingleInstance();
        }
    }
}