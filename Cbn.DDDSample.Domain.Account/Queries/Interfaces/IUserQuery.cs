using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.DDDSampleData.Entities;

namespace Cbn.DDDSample.Domain.Account.Queries.Interfaces
{
    public interface IUserQuery
    {
        Task<UserClaim> GetCurrentUserClaimFromDataAsync();
        Task<UserClaim> GetSignInUserAsync(SignIn args);
    }
}