using System.Threading.Tasks;
using Cbn.Infrastructure.DDDSampleData.Entities;

namespace Cbn.DDDSample.Domain.Account.Shared.Interfaces
{
    public interface ITokenFactory
    {
        Task<string> CreateTokenAsync(User user);
    }
}