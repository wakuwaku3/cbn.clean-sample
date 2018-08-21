using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Commands.Interfaces;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private IUserRepository userRepository;
        private IMapper mapper;

        public CreateUserCommand(
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo)
        {
            var user = await this.userRepository.CreateAsync(userCreationInfo);
            return this.mapper.Map<UserClaim>(user);
        }
    }
}