using System.Collections.Generic;
using System.Linq;
using Cbn.CleanSample.Domain.Common.Configuration;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Cbn.CleanSample.Web.Configuration
{
    public class CleanSampleWebConfig : CleanSampleConfig, IWebConfig
    {
        public CleanSampleWebConfig(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
            this.IsEnableCors = this.configurationRoot.GetValue<bool>(nameof(IsEnableCors));
            this.UseAuthentication = this.configurationRoot.GetValue<bool>(nameof(UseAuthentication));
            this.IsUseSecure = this.configurationRoot.GetValue<bool>(nameof(IsUseSecure));
            this.CorsOrigins = this.configurationRoot.GetValue<string>(nameof(CorsOrigins));
        }

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