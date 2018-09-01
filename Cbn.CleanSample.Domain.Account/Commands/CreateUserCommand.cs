using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Interfaces.Command;
using Cbn.CleanSample.Domain.Account.Interfaces.Repositories;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.CleanSample.Domain.Account.Commands
{
    internal class CreateUserCommand : ICreateUserCommand
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        private IMessageSender messageSender;

        public CreateUserCommand(
            IUserRepository userRepository,
            IMapper mapper,
            IMessageSender messageSender)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.messageSender = messageSender;
        }

        public async Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo)
        {
            var user = await this.userRepository.CreateAsync(userCreationInfo);
            return this.mapper.Map<UserClaim>(user);
        }

        public async Task SendMailForNewUserAsync(UserClaim claim)
        {
            var mail = this.mapper.Map<WelcomeMailArgs>(claim);
            await this.messageSender.SendAsync(mail);
        }
    }
}