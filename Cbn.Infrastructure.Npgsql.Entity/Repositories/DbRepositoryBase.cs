using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity.Repositories
{
    public abstract class DbRepositoryBase<TKey, TEntity> : IDbRepository<TKey, TEntity> where TEntity : class
    {
        private Lazy<IDbContext> dbContextLazy;
        private IDbContext DbContext => this.dbContextLazy.Value;
        protected IDbSet<TEntity> DbSet => this.DbContext.Set<TEntity>();
        protected IQueryable<TEntity> Query => this.DbSet.Query;

        public DbRepositoryBase(Lazy<IDbContext> dbContextLazy)
        {
            this.dbContextLazy = dbContextLazy;
        }

        protected virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        protected virtual void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            return await this.Query.AnyAsync(this.GetKeyExpression(id));
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this.Query.SingleOrDefaultAsync(this.GetKeyExpression(id));
        }

        protected abstract Expression<Func<TEntity, bool>> GetKeyExpression(TKey id);

        protected virtual void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        protected virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }
    }
    public abstract class DbRepositoryBase<TEntity> : DbRepositoryBase<string, TEntity> where TEntity : class
    {
        public DbRepositoryBase(Lazy<IDbContext> dbContextLazy) : base(dbContextLazy) { }
    }
}