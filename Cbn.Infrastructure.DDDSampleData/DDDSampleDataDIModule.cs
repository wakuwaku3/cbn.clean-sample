using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Repositories;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;

namespace Cbn.Infrastructure.DDDSampleData
{
    public class DDDSampleDataDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<HomeRepository>(x => x.As<IHomeRepository>());
            builder.RegisterType<UserRepository>(x => x.As<IUserRepository>());
        }
    }
}