using System.Threading.Tasks;
using Cbn.DDDSample.Application.Models.Account;
using Cbn.DDDSample.Application.Services.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.DDDSample.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private IMapper mapper;

        public AccountController(
            IAccountService accountService,
            IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> SignIn([FromBody] SignInArgs args)
        {
            return await this.accountService.SignInAsync(args);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> SignUp([FromBody] SignUpArgs args)
        {
            return await this.accountService.SignUpAsync(args);
        }

        [Authorize]
        [HttpPost]
        public async Task<string> RefreshToken()
        {
            return await this.accountService.RefreshTokenAsync();
        }
    }
}