using System.Threading.Tasks;
using Cbn.DDDSample.Common.Models;
using Cbn.DDDSample.Domain.Account.Models;

namespace Cbn.DDDSample.Domain.Account.Interfaces.Command
{
    public interface ICreateUserCommand
    {
        Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo);
        Task SendMailForNewUserAsync(UserClaim claim);
    }
}