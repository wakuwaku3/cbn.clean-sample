using Cbn.DDDSample.Common.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cbn.DDDSample.Common.Configuration
{
    public class DDDSampleConfig : IDDDSampleConfig
    {
        private IConfigurationRoot configurationRoot;
        private IConfigurationHelper configurationHelper;

        public DDDSampleConfig(IConfigurationRoot configurationRoot, IConfigurationHelper configurationHelper)
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
        public string Database { get; set; }
        public string AdminConnectionString => this.GetConnectionString("AdminConnection");
        public string ProjectId { get; set; }
        public string TopicId { get; set; }
        public string SubscriptionId { get; set; }

        public string GetConnectionString(string name)
        {
            return this.configurationRoot.GetConnectionString(name);
        }
    }
}