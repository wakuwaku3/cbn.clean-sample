using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Interfaces.Entities;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.CleanSample.Domain.Account.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IUser> CreateAsync(UserCreationInfo user);
    }
}