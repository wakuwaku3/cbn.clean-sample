using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Data.Entity.Interfaces
{
    public interface IDbRepository<TKey, TEntity> where TEntity : class
    { }
    public interface IDbRepository<TEntity> : IDbRepository<string, TEntity> where TEntity : class { }
}