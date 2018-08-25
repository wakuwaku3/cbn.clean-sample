using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;

namespace Cbn.Infrastructure.Npgsql.Entity.Repositories
{
    public abstract class NpgsqlRepositoryBase<TKey, TEntity> : IDbRepository<TKey, TEntity> where TEntity : class
    {
        private Lazy<IDbContext> dbContextLazy;
        private IDbContext DbContext => this.dbContextLazy.Value;
        protected IDbSet<TEntity> DbSet => this.DbContext.Set<TEntity>();
        protected IQueryable<TEntity> Query => this.DbSet.Query;

        public NpgsqlRepositoryBase(Lazy<IDbContext> dbContextLazy)
        {
            this.dbContextLazy = dbContextLazy;
        }
        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public abstract Task<TEntity> GetByIdAsync(TKey id);

        public void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }
    }
    public abstract class NpgsqlRepositoryBase<TEntity> : NpgsqlRepositoryBase<string, TEntity> where TEntity : class
    {
        public NpgsqlRepositoryBase(Lazy<IDbContext> dbContextLazy) : base(dbContextLazy) { }
    }
}