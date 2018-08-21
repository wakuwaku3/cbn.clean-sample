using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Cryptography.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;

namespace Cbn.Infrastructure.DDDSampleData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentBag<User> _cache = new ConcurrentBag<User>();
        private IGuidFactory guidFactory;
        private IMapper mapper;
        private IHashComputer hashComputer;

        public UserRepository(
            IGuidFactory guidFactory,
            IMapper mapper,
            IHashComputer hashComputer)
        {
            this.guidFactory = guidFactory;
            this.mapper = mapper;
            this.hashComputer = hashComputer;
        }
        public async Task<User> CreateAsync(UserCreationInfo createUserArgs)
        {
            var user = this.mapper.Map<User>(createUserArgs);
            user.UserId = this.guidFactory.CreateNew().ToString();
            user.State = UserState.Active;
            user.EncreptedPassword = this.hashComputer.Compute(createUserArgs.Password);
            _cache.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> GetAsync(string userId)
        {
            var user = _cache.SingleOrDefault(u =>
                u.UserId == userId &&
                u.State == UserState.Active);
            return await Task.FromResult(user);
        }

        public async Task<User> GetAsync((string Email, string Password) parameters)
        {
            var (Email, Password) = parameters;
            var encreptedPassword = this.hashComputer.Compute(Password);
            var user = _cache.SingleOrDefault(u =>
                u.Email == Email &&
                u.EncreptedPassword == encreptedPassword &&
                u.State == UserState.Active);
            return await Task.FromResult(user);
        }
    }
}