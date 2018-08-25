using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        IDbTransaction BeginTransaction();

        int ExecuteCommand(IDbQuery query);
        Task<int> ExecuteCommandAsync(IDbQuery query);
        IDbQuery CreateDbQuery(string sqlId = null);
    }
}