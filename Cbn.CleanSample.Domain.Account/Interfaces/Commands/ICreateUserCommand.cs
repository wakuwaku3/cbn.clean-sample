using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.CleanSample.Domain.Common.Models;

namespace Cbn.CleanSample.Domain.Account.Interfaces.Command
{
    public interface ICreateUserCommand
    {
        Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo);
        Task SendMailForNewUserAsync(UserClaim claim);
    }
}