using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.Npgsql.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.DDDSampleData
{
    public class DDDSampleDataContext : NpgsqlDbContext
    {
        public DDDSampleDataContext(DbContextOptions options) : base(options) { }
        public DDDSampleDataContext(DbContextOptions<DDDSampleDataContext> options) : base(options) { }
        public DDDSampleDataContext(DbContextOptions options, IDbQueryCache queryPool) : base(options, queryPool) { }
        public DDDSampleDataContext(DbContextOptions<DDDSampleDataContext> options, IDbQueryCache queryPool) : base(options, queryPool) { }

        public DbSet<User> Users { get; set; }
    }
}