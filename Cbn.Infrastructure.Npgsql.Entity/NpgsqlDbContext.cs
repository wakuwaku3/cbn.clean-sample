using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Entity;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Extensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cbn.Infrastructure.Npgsql.Entity
{
    public abstract class NpgsqlDbContext : DbContext, IDbContext
    {
        private IDbQueryCache queryPool;

        public NpgsqlDbContext(DbContextOptions options) : base(options) { }
        public NpgsqlDbContext(DbContextOptions<NpgsqlDbContext> options) : this(options as DbContextOptions) { }
        public NpgsqlDbContext(DbContextOptions options, IDbQueryCache queryPool) : this(options)
        {
            this.queryPool = queryPool;
        }
        public NpgsqlDbContext(DbContextOptions<NpgsqlDbContext> options, IDbQueryCache queryPool) : this(options as DbContextOptions, queryPool) { }

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

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            return (await this.Database.BeginTransactionAsync())?.GetDbTransaction();
        }

        public async Task<int> ExecuteAsync(IDbQuery query)
        {
            var connection = this.GetDbConnection();
            var transaction = this.Database.CurrentTransaction?.GetDbTransaction();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = query.ToString();
                command.Transaction = transaction;
                return await Task.FromResult(command.ExecuteNonQuery());
            }
        }

        public IDbQuery CreateDbQuery(string sql)
        {
            return new NpgsqlDbQuery(sql);
        }

        public IDbQuery CreateDbQueryById(string sqlId = null)
        {
            return new NpgsqlDbQuery(this.queryPool.GetSqlById(sqlId));
        }

        public IDbConnection GetDbConnection()
        {
            return this.Database.GetDbConnection();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbQuery query)
        {
            var connection = this.GetDbConnection();
            var transaction = this.Database.CurrentTransaction?.GetDbTransaction();
            return await connection.QueryAsync<T>(query.ToString(), query.GetDapperParameters(), transaction);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(IDbQuery query)
        {
            var connection = this.GetDbConnection();
            var transaction = this.Database.CurrentTransaction?.GetDbTransaction();
            return await connection.QuerySingleOrDefaultAsync<T>(query.ToString(), query.GetDapperParameters(), transaction);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(IDbQuery query)
        {
            var connection = this.GetDbConnection();
            var transaction = this.Database.CurrentTransaction?.GetDbTransaction();
            return await connection.QueryFirstOrDefaultAsync<T>(query.ToString(), query.GetDapperParameters(), transaction);
        }
    }
}