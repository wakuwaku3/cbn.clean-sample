using System.Collections.Generic;
using System.Linq;
using Cbn.DDDSample.Domain.Common.Configuration;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Cbn.DDDSample.Web.Configuration
{
    public class DDDSampleWebConfig : DDDSampleConfig, IWebConfig
    {
        public DDDSampleWebConfig(IConfigurationRoot configurationRoot, IConfigurationHelper configurationHelper) : base(configurationRoot, configurationHelper) { }

        public bool IsEnableCors { get; set; }
        public bool UseAuthentication { get; set; }
        public bool IsUseSecure { get; set; }
        public string CorsOrigins { get; set; }

        public IEnumerable<string> GetCorsOrigins()
        {
            return this.CorsOrigins?.Split(",") ?? Enumerable.Empty<string>();
        }

        public void CreateMvcConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}