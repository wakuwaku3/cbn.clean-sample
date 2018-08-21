using System.Text;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cbn.Infrastructure.AspNetCore.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddWebApiService(this IServiceCollection services, IWebConfig config)
        {
            if (config.IsEnableCors)
            {
                services.AddCors();
            }
            if (config.UseAuthentication)
            {
                services
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Audience = config.JwtSecret;
                        options.RequireHttpsMetadata = config.IsUseSecure;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidIssuer = config.JwtSecret,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret)),
                        };
                    });
            }
        }
    }
}