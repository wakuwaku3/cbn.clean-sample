using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Account.Queries.Interfaces;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Queries
{
    public class UserQuery : IUserQuery
    {
        private IMapper mapper;
        private IClaimContext<UserClaim> claimContext;
        private IUserRepository userRepository;

        public UserQuery(
            IMapper mapper,
            IClaimContext<UserClaim> claimContext,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.claimContext = claimContext;
            this.userRepository = userRepository;
        }

        public async Task<UserClaim> GetCurrentUserClaimFromDataAsync()
        {
            var user = await this.userRepository.GetAsync(this.claimContext.Claim.UserId);
            return this.mapper.Map<UserClaim>(user);
        }

        public async Task<UserClaim> GetSignInUserAsync(SignIn args)
        {
            var user = await this.userRepository.GetAsync((args.Email, args.Password));
            return this.mapper.Map<UserClaim>(user);
        }
    }
}