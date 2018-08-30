using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.CleanSample.UseCases.Models.Account;

namespace Cbn.CleanSample.UseCases.Interfaces.Queries
{
    public interface IUserQuery
    {
        Task<UserClaim> GetCurrentUserClaimAsync();
        Task<UserClaim> GetSignInUserClaimAsync(SignInArgs args);
    }
}