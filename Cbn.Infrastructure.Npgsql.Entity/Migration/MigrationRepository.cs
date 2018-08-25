using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity.Migration
{
    public class MigrationRepository : NpgsqlRepositoryBase<MigrationHistory>, IMigrationRepository<MigrationHistory>
    {
        public MigrationRepository(Lazy<IMigrationDbContext> dbContextLazy) : base(new Lazy<IDbContext>(() => dbContextLazy.Value)) { }

        public async Task<bool> ExistsAsync(string id)
        {
            return await this.Query.AnyAsync(x => x.MigrationHistoryId == id);
        }

        public override async Task<MigrationHistory> GetByIdAsync(string id)
        {
            return await this.Query.SingleOrDefaultAsync(x => x.MigrationHistoryId == id);
        }
    }
}