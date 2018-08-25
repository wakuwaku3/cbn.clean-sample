using System.Security.Claims;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Entities;

namespace Cbn.DDDSample.Domain.Account.Shared
{
    public class UserClaimContext : IClaimContext<UserClaim>
    {
        private UserClaim userClaim;
        private IMapper mapper;

        public UserClaimContext(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public UserClaim Claim => this.userClaim;

        public void SetClaims(ClaimsPrincipal claimsPrincipal)
        {
            var jwtClaim = new JwtClaimInfo(claimsPrincipal);
            this.userClaim = this.mapper.Map<UserClaim>(jwtClaim);
        }

        public void SetClaims(UserClaim claim)
        {
            this.userClaim = claim;
        }
    }
}