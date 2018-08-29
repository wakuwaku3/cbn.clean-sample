using System.Data;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbTransactionManager
    {
        Task<IDbTransaction> BeginTransactionAsync();
    }
}