using System.Data;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbTransactionManager
    {
        Task<ITransaction> BeginTransactionAsync();
    }
}