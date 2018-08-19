using System.Threading.Tasks;
using Cbn.Infrastructure.AspNetCore.Extensions;
using Cbn.Infrastructure.AspNetCore.Middlewares.Bases;
using Cbn.Infrastructure.Common.Identities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.AspNetCore.Middlewares
{
    /// <summary>
    /// IdentityContextミドルウェア
    /// </summary>
    public class IdentityContextMiddleware : MiddlewareBase
    {
        private IIdentityContext identityContext;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="requestContext"></param>
        /// <param name="authenticationConfig"></param>
        public IdentityContextMiddleware(
            ILoggerFactory loggerFactory,
            IIdentityContext identityContext) : base(loggerFactory)
        {
            this.identityContext = identityContext;
        }

        /// <summary>
        /// IdentityContextを初期化する
        /// </summary>
        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var id = context.User.GetId();
            if (!string.IsNullOrEmpty(id))
            {
                this.identityContext.Id = id;
            }
            await next.Invoke(context);
        }
    }
}