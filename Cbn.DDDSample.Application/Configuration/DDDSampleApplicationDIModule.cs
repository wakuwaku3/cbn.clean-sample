using Cbn.DDDSample.Application.Services;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Application.Configuration
{
    public class DDDSampleApplicationDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<HomeService>(x => x.As<IHomeService>());
        }
    }
}