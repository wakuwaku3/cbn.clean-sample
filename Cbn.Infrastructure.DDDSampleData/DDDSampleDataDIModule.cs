using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Repositories;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.DDDSampleData
{
    public class DDDSampleDataDIModule : IDIModule
    {
        private string connectionString;

        public DDDSampleDataDIModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<HomeRepository>(x => x.As<IHomeRepository>());
            builder.RegisterType<UserRepository>(x => x.As<IUserRepository>());
            var optionsBuilder = new DbContextOptionsBuilder<DDDSampleDataContext>();
            optionsBuilder.UseNpgsql(this.connectionString);
            builder.RegisterInstance(optionsBuilder.Options);
            builder.RegisterType<DDDSampleDataContext>(x => x.As<IDbContext>().InstancePerLifetimeScope());
            builder.RegisterType<DbQueryCache>(x => x.As<IDbQueryCache>().SingleInstance());
        }
    }
}