using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Data.Entity.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;

namespace Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces
{
    public interface IUserRepository : IDbRepository<User>
    {
        Task<User> GetAsync(string id);
        Task<User> GetAsync((string Email, string Password) singInInfo);
        Task<User> CreateAsync(UserCreationInfo user);
    }
}