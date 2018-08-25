using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cbn.DDDSample.Cli.Configuration
{
    public class CliConfig : IDbConfig, IJwtConfig
    {
        private IConfigurationRoot configurationRoot;
        private IConfigurationHelper configurationHelper;

        public CliConfig(IConfigurationRoot configurationRoot, IConfigurationHelper configurationHelper)
        {
            this.configurationRoot = configurationRoot;
            this.configurationHelper = configurationHelper;
            this.configurationHelper.Map(this, this.configurationRoot);
        }

        public string SqlPoolPath { get; set; }
        public string JwtSecret { get; set; }
        public int JwtExpiresDate { get; set; }
        public string JwtAudience { get; set; }
        public string JwtIssuer { get; set; }
    }
}