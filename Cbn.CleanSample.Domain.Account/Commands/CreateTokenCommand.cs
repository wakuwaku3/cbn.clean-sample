using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Interfaces.Command;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;

namespace Cbn.CleanSample.Domain.Account.Commands
{
    internal class CreateTokenCommand : ICreateTokenCommand
    {
        private IJwtFactory jwtFactory;

        public CreateTokenCommand(IJwtFactory jwtFactory)
        {
            this.jwtFactory = jwtFactory;
        }

        public async Task<string> ExecuteAsync(UserClaim userClaim)
        {
            return await Task.FromResult(this.jwtFactory.Create(userClaim));
        }
    }
}