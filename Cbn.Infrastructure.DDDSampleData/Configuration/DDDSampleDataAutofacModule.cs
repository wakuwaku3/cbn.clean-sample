using Autofac;
using Cbn.Infrastructure.DDDSampleData.Repositories;

namespace Cbn.Infrastructure.DDDSampleData.Configuration
{
    public class DDDSampleDataAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HomeRepository>().As<IHomeRepository>();
        }
    }
}