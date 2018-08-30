using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Interfaces.Command;
using Cbn.DDDSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

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
            return await Task.FromResult(this.jwtFactory.Create(userClaim));
        }
    }
}