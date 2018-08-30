using Cbn.CleanSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.CleanSample.Domain.Common
{
    public class CleanSampleDomainCommonDIModule : IDIModule
    {
        private LifetimeType claimContextLifetimeType;

        public CleanSampleDomainCommonDIModule(LifetimeType claimContextLifetimeType)
        {
            this.claimContextLifetimeType = claimContextLifetimeType;
        }
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserClaimContext>(x => x.As<IClaimContext<UserClaim>>().LifetimeType = this.claimContextLifetimeType);
        }
    }
}