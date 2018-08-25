using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.Npgsql.Entity.Migration
{
    public class MigrationDIModule : IDIModule
    {
        private string connectionString;

        public MigrationDIModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void DefineModule(IDIBuilder builder)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            optionsBuilder.UseNpgsql(this.connectionString);
            builder.RegisterInstance(optionsBuilder.Options);
            builder.RegisterType<MigrationDbContext>(x => x.As<IMigrationDbContext>().InstancePerLifetimeScope());
            builder.RegisterType<MigrationRepository>(x => x.As<IMigrationRepository<MigrationHistory>>());
            builder.RegisterType<MigrationHelper>(x => x.As<IMigrationHelper>());
        }
    }
}