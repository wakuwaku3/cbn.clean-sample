using System.Threading.Tasks;

namespace Cbn.DDDSample.Application.Services.Interfaces
{
    public interface IMigrationService
    {
        Task<int> ExecuteAsync();
    }
}