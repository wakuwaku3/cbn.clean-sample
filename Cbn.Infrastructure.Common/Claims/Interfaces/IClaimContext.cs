using System.Security.Claims;
namespace Cbn.Infrastructure.Common.Claims.Interfaces
{
    public interface IClaimContext<TClaim>
    {
        TClaim Claim { get; }
        void Impersonate(TClaim claim);
        void SetClaims(ClaimsPrincipal claimsPrincipal);
    }
}