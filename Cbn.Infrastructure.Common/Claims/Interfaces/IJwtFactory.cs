using System.Security.Claims;
using Cbn.Infrastructure.Common.Claims.Interfaces;

namespace Cbn.Infrastructure.Common.Claims.Interfaces
{
    public interface IJwtFactory
    {
        string Create(IJwtClaimInfo claimInfo);
        IJwtClaimInfo Create(ClaimsPrincipal claimsPrincipal);
    }
}