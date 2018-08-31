using System.Threading.Tasks;

namespace Cbn.CleanSample.UseCases.Migration
{
    public interface IMigrationUseCase
    {
        Task<int> ExecuteAsync();
    }
}