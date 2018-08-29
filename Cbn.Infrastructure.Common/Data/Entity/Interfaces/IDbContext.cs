using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbContext : IDbTransactionManager, IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        IDbQuery CreateDbQuery(string sql);
        IDbQuery CreateDbQueryById(string sqlId = null);

        Task<int> ExecuteAsync(IDbQuery query);
        Task<IEnumerable<T>> QueryAsync<T>(IDbQuery query);
        Task<T> QuerySingleOrDefaultAsync<T>(IDbQuery query);
        Task<T> QueryFirstOrDefaultAsync<T>(IDbQuery query);
    }
}