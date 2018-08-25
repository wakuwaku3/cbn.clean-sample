using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity.Migration
{
    public class MigrationDbContext : NpgsqlDbContext, IMigrationDbContext
    {
        public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options) { }
        public DbSet<MigrationHistory> MigrationHistories { get; set; }
    }
}