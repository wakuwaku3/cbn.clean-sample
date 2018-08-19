using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Logging.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.AspNetCore.Middlewares.Bases
{
    public abstract class MiddlewareBase : IMiddleware
    {
        protected ILoggerFactory loggerFactory;
        protected ILogger logger;

        public MiddlewareBase(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.GetDefaultLogger();
        }

        public abstract Task InvokeAsync(HttpContext context, RequestDelegate next);
    }
}