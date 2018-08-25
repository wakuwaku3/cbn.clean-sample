using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity
{
    public class NpgsqlDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;

        public NpgsqlDbSet(DbSet<TEntity> dbSet)
        {
            this.dbSet = dbSet;
        }

        public IQueryable<TEntity> Query => this.dbSet;

        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.dbSet.AddRange(entities);
        }

        public TEntity Find(int id)
        {
            return this.dbSet.Find(id);
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> FromSql(IDbQuery query)
        {
            return this.dbSet.FromSql(query.ToString(), query.GetParameters());
        }

        public void Remove(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.dbSet.RemoveRange(entities);
        }

        public async Task<List<TEntity>> ToListAsync()
        {
            return await this.dbSet.ToListAsync();
        }
    }
}