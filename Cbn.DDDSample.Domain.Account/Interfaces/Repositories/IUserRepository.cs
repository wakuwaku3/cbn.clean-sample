using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Interfaces.Entities;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Data.Interfaces;

namespace Cbn.DDDSample.Domain.Account.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IUser> CreateAsync(UserCreationInfo user);
    }
}