using System.Threading.Tasks;
using Cbn.DDDSample.UseCases.Models.Account;

namespace Cbn.DDDSample.UseCases.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(SignUpArgs args);
        Task<string> SignInAsync(SignInArgs args);
        Task<string> RefreshTokenAsync();
    }
}