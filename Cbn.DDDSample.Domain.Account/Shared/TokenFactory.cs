using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Shared.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.JsonWebToken.Entities;
using Cbn.Infrastructure.JsonWebToken.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Shared
{
    public class TokenFactory : ITokenFactory
    {
        private IMapper mapper;
        private IJwtFactory jwtFactory;

        public TokenFactory(
            IMapper mapper,
            IJwtFactory jwtFactory)
        {
            this.mapper = mapper;
            this.jwtFactory = jwtFactory;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            if (user == null)
            {
                return null;
            }
            var claimInfo = this.mapper.Map<JwtClaimInfo>(user);
            return await Task.FromResult(this.jwtFactory.Create(claimInfo));
        }
    }
}