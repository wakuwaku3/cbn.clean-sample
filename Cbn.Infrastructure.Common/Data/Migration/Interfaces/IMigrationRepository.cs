using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Migration.Interfaces
{
    public interface IMigrationRepository<TMigrationHistory> : IDbRepository<TMigrationHistory> where TMigrationHistory : class, IMigrationHistory
    {
        Task<bool> ExistsAsync(string id);
    }
}