using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Common.Models;

namespace Cbn.CleanSample.Domain.Account.Interfaces.Command
{
    public interface ICreateTokenCommand
    {
        Task<string> ExecuteAsync(UserClaim userClaim);
    }
}