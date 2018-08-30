using System.Threading.Tasks;

namespace Cbn.CleanSample.UseCases.Interfaces.Services
{
    public interface IMigrationService
    {
        Task<int> ExecuteAsync();
    }
}