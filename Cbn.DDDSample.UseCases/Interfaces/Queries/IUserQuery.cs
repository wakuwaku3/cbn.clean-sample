using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Common.Models;
using Cbn.DDDSample.UseCases.Models.Account;

namespace Cbn.DDDSample.UseCases.Interfaces.Queries
{
    public interface IUserQuery
    {
        Task<UserClaim> GetCurrentUserClaimAsync();
        Task<UserClaim> GetSignInUserClaimAsync(SignInArgs args);
    }
}