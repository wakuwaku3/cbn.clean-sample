using Cbn.DDDSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Domain.Common
{
    public class DDDSampleDomainCommonDIModule : IDIModule
    {
        private LifetimeType claimContextLifetimeType;

        public DDDSampleDomainCommonDIModule(LifetimeType claimContextLifetimeType)
        {
            this.claimContextLifetimeType = claimContextLifetimeType;
        }
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserClaimContext>(x => x.As<IClaimContext<UserClaim>>().LifetimeType = this.claimContextLifetimeType);
        }
    }
}