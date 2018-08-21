using System.Threading.Tasks;
using Cbn.DDDSample.Application.Models.Account;

namespace Cbn.DDDSample.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(SignUpArgs args);
        Task<string> SignInAsync(SignInArgs args);
        Task<string> RefreshTokenAsync();
    }
}