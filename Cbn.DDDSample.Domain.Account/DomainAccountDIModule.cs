using Cbn.DDDSample.Domain.Account.Commands;
using Cbn.DDDSample.Domain.Account.Interfaces.Command;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Domain.Account
{
    public class DomainAccountDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<CreateTokenCommand>(x => x.As<ICreateTokenCommand>());
            builder.RegisterType<CreateUserCommand>(x => x.As<ICreateUserCommand>());
        }
    }
}