using System.Threading.Tasks;
using Cbn.DDDSample.Common.Models;
using Cbn.DDDSample.Domain.Account.Interfaces.Command;
using Cbn.DDDSample.Domain.Account.Interfaces.Repositories;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        private IPublisher publisher;

        public CreateUserCommand(
            IUserRepository userRepository,
            IMapper mapper,
            IPublisher publisher)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo)
        {
            var user = await this.userRepository.CreateAsync(userCreationInfo);
            return this.mapper.Map<UserClaim>(user);
        }

        public async Task SendMailForNewUserAsync(UserClaim claim)
        {
            var mail = this.mapper.Map<WelcomeMailArgs>(claim);
            await this.publisher.PublishAsync(mail);
        }
    }
}