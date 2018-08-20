using Cbn.DDDSample.Domain.Home;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Domain.Configuration
{
    public class DDDSampleDomainDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<Query>(x => x.As<IQuery1>().InstancePerLifetimeScope());
            builder.RegisterType<Query>(x => x.As<IQuery2>().InstancePerMatchingLifetimeScope("scope1"));
            builder.RegisterType<Query>(x => x.As<IQuery3>().InstancePerMatchingLifetimeScope("scope2"));
            builder.RegisterType<Query>(x => x.As<IQuery4>().InstancePerDependency());
            builder.RegisterType<Query>(x => x.As<IQuery5>().SingleInstance());
        }
    }
}