using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace Cbn.Infrastructure.AspNetCore.Configuration.Interfaces
{
    public interface IWebConfig
    {
        bool IsEnableCors { get; }
        bool UseAuthentication { get; }
        string JwtSecret { get; }
        bool IsUseSecure { get; }

        IEnumerable<string> GetCorsOrigins();
        void CreateMvcConfigureRoutes(IRouteBuilder routes);
    }
}