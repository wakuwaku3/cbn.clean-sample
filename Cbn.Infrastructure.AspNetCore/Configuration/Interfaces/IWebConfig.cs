using System.Collections.Generic;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace Cbn.Infrastructure.AspNetCore.Configuration.Interfaces
{
    public interface IWebConfig : IJwtConfig
    {
        bool IsEnableCors { get; }
        bool UseAuthentication { get; }
        bool IsUseSecure { get; }

        IEnumerable<string> GetCorsOrigins();
        void CreateMvcConfigureRoutes(IRouteBuilder routes);
    }
}