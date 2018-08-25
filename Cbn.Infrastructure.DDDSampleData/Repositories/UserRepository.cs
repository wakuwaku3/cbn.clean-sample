using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Cryptography.Interfaces;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;
using Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces;
using Cbn.Infrastructure.Npgsql.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cbn.Infrastructure.DDDSampleData.Repositories
{
    public class UserRepository : DbRepositoryBase<User>, IUserRepository
    {
        private IGuidFactory guidFactory;
        private IMapper mapper;
        private IHashComputer hashComputer;

        public UserRepository(
            Lazy<IDbContext> dbContextLazy,
            IGuidFactory guidFactory,
            IMapper mapper,
            IHashComputer hashComputer) : base(dbContextLazy)
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
            this.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> GetAsync(string userId)
        {
            return await this.Query.SingleOrDefaultAsync(u =>
                u.UserId == userId &&
                u.State == UserState.Active);
        }

        public async Task<User> GetAsync((string Email, string Password) parameters)
        {
            var (Email, Password) = parameters;
            var encreptedPassword = this.hashComputer.Compute(Password);
            return await this.Query.SingleOrDefaultAsync(u =>
                u.Email == Email &&
                u.EncreptedPassword == encreptedPassword &&
                u.State == UserState.Active);
        }

        protected override Expression<Func<User, bool>> GetKeyExpression(string id)
        {
            return e => e.UserId == id;
        }
    }
}