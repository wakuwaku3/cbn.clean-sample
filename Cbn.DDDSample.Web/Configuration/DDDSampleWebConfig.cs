using System.Collections.Generic;
using System.Linq;
using Cbn.DDDSample.Common;
using Cbn.Infrastructure.AspNetCore.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Messaging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Cbn.DDDSample.Web.Configuration
{
    public class DDDSampleWebConfig : DDDSampleConfig, IWebConfig
    {
        private IConfigurationRoot configurationRoot;
        private IConfigurationHelper configurationHelper;

        public DDDSampleWebConfig(IConfigurationRoot configurationRoot, IConfigurationHelper configurationHelper) : base(configurationRoot, configurationHelper)
        { }

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