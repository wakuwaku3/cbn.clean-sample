using System.Threading.Tasks;
using Cbn.CleanSample.UseCases.Models.Account;

namespace Cbn.CleanSample.UseCases.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(SignUpArgs args);
        Task<string> SignInAsync(SignInArgs args);
        Task<string> RefreshTokenAsync();
    }
}