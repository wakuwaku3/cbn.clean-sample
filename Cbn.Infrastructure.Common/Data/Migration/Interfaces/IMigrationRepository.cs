using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Migration.Interfaces
{
    public interface IMigrationRepository<TMigrationHistory> : IRepository<TMigrationHistory> where TMigrationHistory : class, IMigrationHistory
    {
        Task<bool> ExistsAsync(string id);
        Task<TMigrationHistory> GetByIdAsync(string id);
        Task AddAsync(TMigrationHistory entity);
    }
}