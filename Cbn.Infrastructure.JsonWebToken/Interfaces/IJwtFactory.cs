using Cbn.Infrastructure.JsonWebToken.Entities;

namespace Cbn.Infrastructure.JsonWebToken.Interfaces
{
    public interface IJwtFactory
    {
        string Create(JwtClaimInfo claimInfo);
    }
}