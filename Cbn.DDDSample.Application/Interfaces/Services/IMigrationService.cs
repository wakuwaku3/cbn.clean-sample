using System.Threading.Tasks;

namespace Cbn.DDDSample.Application.Interfaces.Services
{
    public interface IMigrationService
    {
        Task<int> ExecuteAsync();
    }
}