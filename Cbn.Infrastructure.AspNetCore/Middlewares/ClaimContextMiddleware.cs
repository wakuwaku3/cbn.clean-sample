using System.Threading.Tasks;
using Cbn.Infrastructure.AspNetCore.Extensions;
using Cbn.Infrastructure.AspNetCore.Middlewares.Bases;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.AspNetCore.Middlewares
{
    /// <summary>
    /// IdentityContextミドルウェア
    /// </summary>
    public class ClaimContextMiddleware<TClaim> : MiddlewareBase
    {
        private IClaimContext<TClaim> claimContext;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="requestContext"></param>
        /// <param name="authenticationConfig"></param>
        public ClaimContextMiddleware(
            ILoggerFactory loggerFactory,
            IClaimContext<TClaim> claimContext) : base(loggerFactory)
        {
            this.claimContext = claimContext;
        }

        /// <summary>
        /// IdentityContextを初期化する
        /// </summary>
        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            this.claimContext.SetClaims(context.User);
            await next.Invoke(context);
        }
    }
}