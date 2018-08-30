using Cbn.CleanSample.UseCases.Interfaces.Services;
using Cbn.CleanSample.UseCases.Services;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.CleanSample.UseCases
{
    public class UseCasesDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<AccountService>(x => x.As<IAccountService>());
            builder.RegisterType<MigrationService>(x => x.As<IMigrationService>());
        }
    }
}