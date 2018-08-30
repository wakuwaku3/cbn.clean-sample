using Cbn.DDDSample.Domain.Account.Interfaces.Repositories;
using Cbn.DDDSample.UseCases.Interfaces.Queries;
using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Repositories;
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
            builder.RegisterType<UserRepository>(x => x.As<IUserRepository>().As<IUserQuery>());
            var optionsBuilder = new DbContextOptionsBuilder<DDDSampleDataContext>();
            optionsBuilder.UseNpgsql(this.connectionString);
            builder.RegisterInstance(optionsBuilder.Options);
            builder.RegisterType<DDDSampleDataContext>(x => x.As<IDbContext>().As<IDbTransactionManager>().InstancePerLifetimeScope());
            builder.RegisterType<DbQueryCache>(x => x.As<IDbQueryCache>().SingleInstance());
        }
    }
}