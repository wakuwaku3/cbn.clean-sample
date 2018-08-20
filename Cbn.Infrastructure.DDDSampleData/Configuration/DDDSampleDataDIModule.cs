using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Repositories;

namespace Cbn.Infrastructure.DDDSampleData.Configuration
{
    public class DDDSampleDataDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<HomeRepository>(x => x.As<IHomeRepository>());
        }
    }

}