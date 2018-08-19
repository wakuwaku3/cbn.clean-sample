using Autofac;
using Cbn.DDDSample.Application.Services;

namespace Cbn.DDDSample.Application.Configuration
{
    public class DDDSampleApplicationAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HomeService>().As<IHomeService>();
        }
    }
}