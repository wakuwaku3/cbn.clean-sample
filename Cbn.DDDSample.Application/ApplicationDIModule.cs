using Cbn.DDDSample.Application.Interfaces.Services;
using Cbn.DDDSample.Application.Services;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Application
{
    public class ApplicationDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<AccountService>(x => x.As<IAccountService>());
            builder.RegisterType<MigrationService>(x => x.As<IMigrationService>());
        }
    }
}