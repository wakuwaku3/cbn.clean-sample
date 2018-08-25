using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Entity;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cbn.Infrastructure.Npgsql.Entity
{
    public class NpgsqlDbContext : DbContext, IDbContext
    {
        private IDbQueryCache queryPool;

        public NpgsqlDbContext(DbContextOptions options) : base(options) { }
        public NpgsqlDbContext(DbContextOptions options, IDbQueryCache queryPool) : this(options)
        {
            this.queryPool = queryPool;
        }

        public override int SaveChanges()
        {
            this.PreSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.PreSaveChanges();
            return await base.SaveChangesAsync();
        }

        protected virtual void PreSaveChanges()
        {
            foreach (var item in this.ChangeTracker.Entries())
            {
                this.PreSaveChanges(item);
            }
        }

        protected virtual void PreSaveChanges(EntityEntry item) { }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return new NpgsqlDbSet<TEntity>(base.Set<TEntity>());
        }

        public IDbTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction().GetDbTransaction();
        }

        public int ExecuteCommand(IDbQuery query)
        {
            return this.Database.ExecuteSqlCommand(query.ToString(), query.GetParameters());
        }

        public async Task<int> ExecuteCommandAsync(IDbQuery query)
        {
            return await this.Database.ExecuteSqlCommandAsync(query.ToString(), query.GetParameters());
        }
        private readonly string sql;

        public IDbQuery CreateDbQuery(string sql)
        {
            return new NpgsqlDbQuery(sql);
        }

        public IDbQuery CreateDbQueryById(string sqlId = null)
        {
            return new NpgsqlDbQuery(this.queryPool.GetSqlById(sqlId));
        }
    }
}