using System.Threading.Tasks;

namespace Cbn.DDDSample.UseCases.Interfaces.Services
{
    public interface IMigrationService
    {
        Task<int> ExecuteAsync();
    }
}