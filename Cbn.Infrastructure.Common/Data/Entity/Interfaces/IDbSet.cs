using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbSet
    {

    }
    public interface IDbSet<TEntity> : IDbSet where TEntity : class
    {
        IQueryable<TEntity> Query { get; }
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity Find(int id);
        Task<TEntity> FindAsync(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<List<TEntity>> ToListAsync();
        IQueryable<TEntity> FromSql(IDbQuery query);
    }
}