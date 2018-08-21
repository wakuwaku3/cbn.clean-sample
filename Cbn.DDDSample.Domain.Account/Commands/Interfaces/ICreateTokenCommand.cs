using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;

namespace Cbn.DDDSample.Domain.Account.Commands.Interfaces
{
    public interface ICreateTokenCommand
    {
        Task<string> ExecuteAsync(UserClaim userClaim);
    }
}