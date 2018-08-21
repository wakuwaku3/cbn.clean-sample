using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Interfaces;

namespace Cbn.Infrastructure.JsonWebToken
{
    public class JwtDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<JwtFactory>(x => x.As<IJwtFactory>().InstancePerLifetimeScope());
        }
    }
}