using System.Collections.Generic;
using System.Linq;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Cbn.DDDSample.Web.Configuration
{
    public class DDDSampleWebConfig : IWebConfig, IDbConfig
    {
        private IConfigurationRoot configurationRoot;
        private IConfigurationHelper configurationHelper;

        public DDDSampleWebConfig(IConfigurationRoot configurationRoot, IConfigurationHelper configurationHelper)
        {
            this.configurationRoot = configurationRoot;
            this.configurationHelper = configurationHelper;
            this.configurationHelper.Map(this, this.configurationRoot);
        }

        public bool IsEnableCors { get; set; }
        public bool UseAuthentication { get; set; }
        public string JwtSecret { get; set; }
        public bool IsUseSecure { get; set; }
        public string CorsOrigins { get; set; }
        public int JwtExpiresDate { get; set; }
        public string JwtAudience { get; set; }
        public string JwtIssuer { get; set; }
        public string SqlPoolPath { get; set; }

        public IEnumerable<string> GetCorsOrigins()
        {
            return this.CorsOrigins?.Split(",") ?? Enumerable.Empty<string>();
        }

        public void CreateMvcConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }

        public string GetConnectionString(string name)
        {
            return this.configurationRoot.GetConnectionString(name);
        }
    }
}