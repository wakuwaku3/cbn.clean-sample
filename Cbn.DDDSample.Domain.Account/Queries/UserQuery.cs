using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Account.Queries.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Identities.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Queries
{
    public class UserQuery : IUserQuery
    {
        private IMapper mapper;
        private IIdentityContext identityContext;
        private IUserRepository userRepository;

        public UserQuery(
            IMapper mapper,
            IIdentityContext identityContext,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.identityContext = identityContext;
            this.userRepository = userRepository;
        }

        public async Task<UserClaim> GetCurrentUserClaimAsync()
        {
            // ToDo
            return await Task.FromResult(new UserClaim { UserId = this.identityContext.Id });
        }

        public async Task<UserClaim> GetCurrentUserClaimFromDataAsync()
        {
            var user = await this.userRepository.GetAsync(this.identityContext.Id);
            return this.mapper.Map<UserClaim>(user);
        }

        public async Task<UserClaim> GetSignInUserAsync(SignIn args)
        {
            var user = await this.userRepository.GetAsync((args.Email, args.Password));
            return this.mapper.Map<UserClaim>(user);
        }
    }
}