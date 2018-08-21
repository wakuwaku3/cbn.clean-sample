using System.Threading.Tasks;
using Cbn.Infrastructure.DDDSampleData.Entities;
using Cbn.Infrastructure.DDDSampleData.Models.User;

namespace Cbn.Infrastructure.DDDSampleData.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync((string Email, string Password) singInInfo);
        Task<User> GetAsync(string userId);
        Task<User> CreateAsync(UserCreationInfo user);
    }
}