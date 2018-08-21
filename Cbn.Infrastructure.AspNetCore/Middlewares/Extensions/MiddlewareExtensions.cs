using System.Linq;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Cbn.Infrastructure.AspNetCore.Middlewares.Extensions
{
    /// <summary>
    /// ミドルウェア登録用拡張メソッド
    /// </summary>
    public static class MiddlewareExtensions
    {
        private static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder) => builder.UseMiddleware<RequestLoggerMiddleware>();
        private static IApplicationBuilder UseIdentityContext<TClaim>(this IApplicationBuilder builder) => builder.UseMiddleware<ClaimContextMiddleware<TClaim>>();
        public static IApplicationBuilder UseWebApiServiceMiddlewares<TClaim>(this IApplicationBuilder builder, IWebConfig config)
        {
            builder.UseMiddleware<RequestLoggerMiddleware>();
            if (config.IsEnableCors)
            {
                builder.UseCors(option =>
                {
                    option
                        .WithOrigins(config.GetCorsOrigins().ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            }
            if (config.UseAuthentication)
            {
                builder.UseAuthentication();
                builder.UseIdentityContext<TClaim>();
            }

            builder.UseMvc(config.CreateMvcConfigureRoutes);

            return builder;
        }
    }
}