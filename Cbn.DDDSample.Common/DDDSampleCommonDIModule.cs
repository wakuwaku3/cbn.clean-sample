using Cbn.DDDSample.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Common
{
    public class DDDSampleCommonDIModule : IDIModule
    {
        private LifetimeType claimContextLifetimeType;

        public DDDSampleCommonDIModule(LifetimeType claimContextLifetimeType)
        {
            this.claimContextLifetimeType = claimContextLifetimeType;
        }
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserClaimContext>(x => x.As<IClaimContext<UserClaim>>().LifetimeType = this.claimContextLifetimeType);
        }
    }
}