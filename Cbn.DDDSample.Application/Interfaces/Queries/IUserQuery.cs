using System.Threading.Tasks;
using Cbn.DDDSample.Application.Models.Account;
using Cbn.DDDSample.Common.Models;
using Cbn.DDDSample.Domain.Account.Models;

namespace Cbn.DDDSample.Application.Interfaces.Queries
{
    public interface IUserQuery
    {
        Task<UserClaim> GetCurrentUserClaimAsync();
        Task<UserClaim> GetSignInUserClaimAsync(SignInArgs args);
    }
}