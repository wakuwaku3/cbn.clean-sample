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
                        options.Audience = config.JwtAudience;
                        options.ClaimsIssuer = config.JwtIssuer;
                        options.RequireHttpsMetadata = config.IsUseSecure;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = config.JwtIssuer,
                            ValidAudience = config.JwtAudience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret)),
                        };
                    });
            }
        }
    }
}