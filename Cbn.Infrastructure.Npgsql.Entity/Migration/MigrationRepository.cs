using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity.Migration
{
    public class MigrationRepository : DbRepositoryBase<MigrationHistory>, IMigrationRepository<MigrationHistory>
    {
        public MigrationRepository(Lazy<IMigrationDbContext> dbContextLazy) : base(new Lazy<IDbContext>(() => dbContextLazy.Value)) { }

        protected override Expression<Func<MigrationHistory, bool>> GetKeyExpression(string id)
        {
            return e => e.MigrationHistoryId == id;
        }

        void IMigrationRepository<MigrationHistory>.Add(MigrationHistory entity)
        {
            this.Add(entity);
        }
    }
}