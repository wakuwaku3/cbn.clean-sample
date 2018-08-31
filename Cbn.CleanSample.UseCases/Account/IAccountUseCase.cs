using System.Threading.Tasks;

namespace Cbn.CleanSample.UseCases.Account
{
    public interface IAccountUseCase
    {
        Task<string> SignUpAsync(SignUpArgs args);
        Task<string> SignInAsync(SignInRequest args);
        Task<string> RefreshTokenAsync();
    }
}