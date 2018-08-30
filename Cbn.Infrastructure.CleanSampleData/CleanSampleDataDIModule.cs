using Cbn.CleanSample.Domain.Account.Interfaces.Repositories;
using Cbn.CleanSample.UseCases.Interfaces.Queries;
using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.CleanSampleData.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.CleanSampleData
{
    public class CleanSampleDataDIModule : IDIModule
    {
        private string connectionString;

        public CleanSampleDataDIModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserRepository>(x => x.As<IUserRepository>().As<IUserQuery>());
            var optionsBuilder = new DbContextOptionsBuilder<CleanSampleDataContext>();
            optionsBuilder.UseNpgsql(this.connectionString);
            builder.RegisterInstance(optionsBuilder.Options);
            builder.RegisterType<CleanSampleDataContext>(x => x.As<IDbContext>().As<IDbTransactionManager>().InstancePerLifetimeScope());
            builder.RegisterType<DbQueryCache>(x => x.As<IDbQueryCache>().SingleInstance());
        }
    }
}