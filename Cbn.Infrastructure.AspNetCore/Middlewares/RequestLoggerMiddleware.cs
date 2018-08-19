using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cbn.Infrastructure.AspNetCore.Middlewares.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.AspNetCore.Middlewares
{
    public class RequestLoggerMiddleware : MiddlewareBase
    {
        public RequestLoggerMiddleware(ILoggerFactory loggerFactory) : base(loggerFactory) { }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            this.logger.LogInformation($"Handling request: {context.Request.Path}");
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error handling request.");
                throw;
            }
            finally
            {
                this.logger.LogInformation("Finished handling request.");
            }
        }
    }
}