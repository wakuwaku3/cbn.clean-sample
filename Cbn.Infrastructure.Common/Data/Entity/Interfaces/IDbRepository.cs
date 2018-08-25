using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbRepository<TKey, TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
    public interface IDbRepository<TEntity> : IDbRepository<string, TEntity> where TEntity : class { }
}