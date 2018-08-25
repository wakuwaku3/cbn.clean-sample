using System.Security.Claims;
namespace Cbn.Infrastructure.Common.Claims.Interfaces
{
    public interface IClaimContext<TClaim>
    {
        TClaim Claim { get; }
        void SetClaims(TClaim claim);
        void SetClaims(ClaimsPrincipal claimsPrincipal);
    }
}