using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;

namespace Cbn.DDDSample.Domain.Account.Commands.Interfaces
{
    public interface ICreateUserCommand
    {
        Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo);
    }
}