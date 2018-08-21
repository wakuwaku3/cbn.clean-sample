using System.Threading.Tasks;
using Cbn.DDDSample.Application.Models.Account;
using Cbn.DDDSample.Application.Services.Interfaces;
using Cbn.DDDSample.Domain.Account.Commands.Interfaces;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Account.Queries.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Models.User;

namespace Cbn.DDDSample.Application.Services
{
    public class AccountService : IAccountService
    {
        private IMapper mapper;
        private ICreateUserCommand createUserCommand;
        private ICreateTokenCommand createTokenCommand;
        private IUserQuery userQuery;

        public AccountService(
            IMapper mapper,
            ICreateUserCommand createUserCommand,
            ICreateTokenCommand createTokenCommand,
            IUserQuery userQuery)
        {
            this.mapper = mapper;
            this.createUserCommand = createUserCommand;
            this.createTokenCommand = createTokenCommand;
            this.userQuery = userQuery;
        }

        public async Task<string> SignUpAsync(SignUpArgs args)
        {
            var creationInfo = this.mapper.Map<UserCreationInfo>(args);
            var claim = await this.createUserCommand.ExecuteAsync(creationInfo);
            return await this.createTokenCommand.ExecuteAsync(claim);
        }

        public async Task<string> SignInAsync(SignInArgs args)
        {
            var signIn = this.mapper.Map<SignIn>(args);
            var claim = await this.userQuery.GetSignInUserAsync(signIn);
            return await this.createTokenCommand.ExecuteAsync(claim);
        }

        public async Task<string> RefreshTokenAsync()
        {
            var claim = await this.userQuery.GetCurrentUserClaimFromDataAsync();
            return await this.createTokenCommand.ExecuteAsync(claim);
        }
    }
}