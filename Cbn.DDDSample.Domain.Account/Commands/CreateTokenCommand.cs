using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Commands.Interfaces;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.JsonWebToken.Entities;
using Cbn.Infrastructure.JsonWebToken.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Commands
{
    public class CreateTokenCommand : ICreateTokenCommand
    {
        private IJwtFactory jwtFactory;
        private IMapper mapper;

        public CreateTokenCommand(IJwtFactory jwtFactory, IMapper mapper)
        {
            this.jwtFactory = jwtFactory;
            this.mapper = mapper;
        }

        public async Task<string> ExecuteAsync(UserClaim userClaim)
        {
            var claimInfo = this.mapper.Map<JwtClaimInfo>(userClaim);
            return await Task.FromResult(this.jwtFactory.Create(claimInfo));
        }
    }
}