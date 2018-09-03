using Cbn.Infrastructure.CleanSampleData.Entities;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.CleanSampleData
{
    public class CleanSampleDataContext : NpgsqlDbContext
    {
        public CleanSampleDataContext(DbContextOptions options) : base(options) { }
        public CleanSampleDataContext(DbContextOptions<CleanSampleDataContext> options) : base(options) { }
        public CleanSampleDataContext(DbContextOptions options, IDbQueryCache queryPool) : base(options, queryPool) { }
        public CleanSampleDataContext(DbContextOptions<CleanSampleDataContext> options, IDbQueryCache queryPool) : base(options, queryPool) { }

        public DbSet<User> Users { get; set; }
    }
}