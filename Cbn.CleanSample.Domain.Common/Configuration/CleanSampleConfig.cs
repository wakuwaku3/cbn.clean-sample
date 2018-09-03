using System;
using System.Collections.Generic;
using System.Linq;
using Cbn.CleanSample.Domain.Common.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Messaging;
using Microsoft.Extensions.Configuration;

namespace Cbn.CleanSample.Domain.Common.Configuration
{
    public class CleanSampleConfig : ICleanSampleConfig
    {
        protected IConfigurationRoot configurationRoot;

        public CleanSampleConfig(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
            this.SqlPoolPath = this.configurationRoot.GetValue<string>(nameof(this.SqlPoolPath));
            this.JwtSecret = this.configurationRoot.GetValue<string>(nameof(this.JwtSecret));
            this.JwtExpiresDate = this.configurationRoot.GetValue<int>(nameof(this.JwtExpiresDate));
            this.JwtAudience = this.configurationRoot.GetValue<string>(nameof(this.JwtAudience));
            this.JwtIssuer = this.configurationRoot.GetValue<string>(nameof(this.JwtIssuer));
            this.Database = this.configurationRoot.GetValue<string>(nameof(this.Database));
            this.AdminConnectionString = this.GetConnectionString("AdminConnection");
            this.SystemDateTime = this.configurationRoot.GetValue<DateTime?>(nameof(this.SystemDateTime));
            this.AwsAccessKeyId = this.configurationRoot.GetValue<string>(nameof(this.AwsAccessKeyId));
            this.AwsSecretAccessKey = this.configurationRoot.GetValue<string>(nameof(this.AwsSecretAccessKey));
            this.ServiceURL = this.configurationRoot.GetValue<string>(nameof(this.ServiceURL));
            this.DefaultSQSQueueSetting = new SQSQueueSetting(this.configurationRoot.GetSection(nameof(this.DefaultSQSQueueSetting)));
            this.SQSQueueSettings = this.configurationRoot.GetSection(nameof(this.SQSQueueSettings)).GetChildren().Select(x => new SQSQueueSetting(x)).ToArray();
            this.SQSQueueSettingDictionary = this.SQSQueueSettings.SelectMany(x => x.TargetMessageTypes.Select(y => new { key = y, value = x })).ToDictionary(x => x.key, x => x.value);
            this.MaxConcurrencyReceive = this.configurationRoot.GetValue<int>(nameof(this.MaxConcurrencyReceive));
        }
        public string SqlPoolPath { get; }
        public string JwtSecret { get; }
        public int JwtExpiresDate { get; }
        public string JwtAudience { get; }
        public string JwtIssuer { get; }
        public string Database { get; }
        public string AdminConnectionString { get; }
        public DateTime? SystemDateTime { get; }
        public string AwsAccessKeyId { get; }
        public string AwsSecretAccessKey { get; }
        public string ServiceURL { get; }
        public SQSQueueSetting DefaultSQSQueueSetting { get; }
        public IEnumerable<SQSQueueSetting> SQSQueueSettings { get; }
        public IDictionary<string, SQSQueueSetting> SQSQueueSettingDictionary { get; }

        public int MaxConcurrencyReceive { get; }

        public string GetConnectionString(string name)
        {
            return this.configurationRoot.GetConnectionString(name);
        }
    }
}