using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Common.Models;

namespace Cbn.DDDSample.Domain.Account.Interfaces.Command
{
    public interface ICreateUserCommand
    {
        Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo);
        Task SendMailForNewUserAsync(UserClaim claim);
    }
}