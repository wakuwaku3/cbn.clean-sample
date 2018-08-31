using Cbn.CleanSample.UseCases.Account;
using Cbn.CleanSample.UseCases.Migration;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.CleanSample.UseCases
{
    public class UseCasesDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<AccountUseCase>(x => x.As<IAccountUseCase>());
            builder.RegisterType<MigrationUseCase>(x => x.As<IMigrationUseCase>());
        }
    }
}