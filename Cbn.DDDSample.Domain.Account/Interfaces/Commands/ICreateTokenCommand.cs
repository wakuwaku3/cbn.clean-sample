using System.Threading.Tasks;
using Cbn.DDDSample.Common.Models;

namespace Cbn.DDDSample.Domain.Account.Interfaces.Command
{
    public interface ICreateTokenCommand
    {
        Task<string> ExecuteAsync(UserClaim userClaim);
    }
}