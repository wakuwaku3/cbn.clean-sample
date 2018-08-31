using System;
using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Interfaces.Command;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.CleanSample.UseCases.Interfaces.Queries;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.CleanSample.UseCases.Account
{
    internal class AccountUseCase : IAccountUseCase
    {
        private Lazy<IDbContext> dbContextLazy;
        private IMapper mapper;
        private ICreateUserCommand createUserCommand;
        private ICreateTokenCommand createTokenCommand;
        private IUserQuery userQuery;

        public AccountUseCase(
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

        public async Task<string> SignInAsync(SignInRequest args)
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