using System;
using Amazon.SQS;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Cbn.Infrastructure.SQS.Interfaces;

namespace Cbn.Infrastructure.SQS
{
    public class SQSClientProvider : ISQSClientProvider
    {
        private ISQSConfig config;
        private AmazonSQSConfig awsConfig;
        private Lazy<AmazonSQSClient> sqsClientLazy;

        public SQSClientProvider(ISQSConfig config)
        {
            this.config = config;
            this.awsConfig = new AmazonSQSConfig
            {
                ServiceURL = this.config.ServiceURL,
            };
            this.sqsClientLazy = new Lazy<AmazonSQSClient>(() => new AmazonSQSClient(this.config.AwsAccessKeyId, this.config.AwsSecretAccessKey, this.awsConfig), true);
        }

        public IAmazonSQS SQSClient => this.sqsClientLazy.Value;
    }
}