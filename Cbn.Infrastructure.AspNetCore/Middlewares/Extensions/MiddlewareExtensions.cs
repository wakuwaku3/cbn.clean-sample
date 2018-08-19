using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static IApplicationBuilder UseIdentityContext(this IApplicationBuilder builder) => builder.UseMiddleware<IdentityContextMiddleware>();
        public static IApplicationBuilder UseWebApiServiceMiddlewares(this IApplicationBuilder builder, IWebConfig config)
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
                builder.UseIdentityContext();
            }

            builder.UseMvc(config.CreateMvcConfigureRoutes);

            return builder;
        }
    }
}