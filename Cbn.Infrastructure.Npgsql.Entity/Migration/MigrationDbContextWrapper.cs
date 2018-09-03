using System.Threading;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Wrapper;

namespace Cbn.Infrastructure.Npgsql.Entity.Migration
{
    public class MigrationDbContextWrapper : DbContextWrapper<MigrationDbContext>, IMigrationDbContext
    {
        public MigrationDbContextWrapper(MigrationDbContext context, CancellationTokenSource tokenSource, IDbQueryCache queryPool) : base(context, tokenSource, queryPool) { }
    }
}