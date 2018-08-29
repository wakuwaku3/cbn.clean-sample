using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

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