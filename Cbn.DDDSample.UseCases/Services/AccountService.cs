using System;
using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Interfaces.Command;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.DDDSample.Domain.Common.Models;
using Cbn.DDDSample.UseCases.Interfaces.Queries;
using Cbn.DDDSample.UseCases.Interfaces.Services;
using Cbn.DDDSample.UseCases.Models.Account;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.DDDSample.UseCases.Services
{
    public class AccountService : IAccountService
    {
        private Lazy<IDbContext> dbContextLazy;
        private IMapper mapper;
        private ICreateUserCommand createUserCommand;
        private ICreateTokenCommand createTokenCommand;
        private IUserQuery userQuery;

        public AccountService(
            Lazy<IDbContext> dbContextLazy,
            IMapper mapper,
            ICreateUserCommand createUserCommand,
            ICreateTokenCommand createTokenCommand,
            IUserQuery userQuery)
        {
            this.dbContextLazy = dbContextLazy;
            this.mapper = mapper;
            this.createUserCommand = createUserCommand;
            this.createTokenCommand = createTokenCommand;
            this.userQuery = userQuery;
        }

        private IDbContext DbContext => this.dbContextLazy.Value;

        public async Task<string> SignUpAsync(SignUpArgs args)
        {
            var creationInfo = this.mapper.Map<UserCreationInfo>(args);
            var claim = await ExecuteAsync();
            await this.createUserCommand.SendMailForNewUserAsync(claim);
            return await this.createTokenCommand.ExecuteAsync(claim);

            async Task<UserClaim> ExecuteAsync()
            {
                var trn = await this.DbContext.BeginTransactionAsync();
                try
                {
                    var res = await this.createUserCommand.ExecuteAsync(creationInfo);
                    await this.DbContext.SaveChangesAsync();
                    trn.Commit();
                    return res;
                }
                catch (System.Exception)
                {
                    trn.Rollback();
                    throw;
                }
                finally
                {
                    trn.Dispose();
                }
            }
        }

        public async Task<string> SignInAsync(SignInArgs args)
        {
            var userClaim = await this.userQuery.GetSignInUserClaimAsync(args);
            return await this.createTokenCommand.ExecuteAsync(userClaim);
        }

        public async Task<string> RefreshTokenAsync()
        {
            var userClaim = await this.userQuery.GetCurrentUserClaimAsync();
            return await this.createTokenCommand.ExecuteAsync(userClaim);
        }
    }
}