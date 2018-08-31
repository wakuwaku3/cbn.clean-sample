using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Interfaces.Command;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.CleanSample.Domain.Account.Commands
{
    internal class CreateTokenCommand : ICreateTokenCommand
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
            return await Task.FromResult(this.jwtFactory.Create(userClaim));
        }
    }
}