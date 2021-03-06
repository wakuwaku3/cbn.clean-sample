using System.Runtime.CompilerServices;
using Cbn.CleanSample.Domain.Account.Commands;
using Cbn.CleanSample.Domain.Account.Interfaces.Command;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
[assembly : InternalsVisibleTo("Cbn.CleanSample.Domain.Account.Tests")]
namespace Cbn.CleanSample.Domain.Account
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