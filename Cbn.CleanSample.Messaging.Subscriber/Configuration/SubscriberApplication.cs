using System;
using System.Threading;
using System.Threading.Tasks;
using Cbn.CleanSample.UseCases.Migration;
using Cbn.Infrastructure.Common.Messaging.Interfaces;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Cbn.CleanSample.Messaging.Subscriber.Configuration
{
    public class SubscriberApplication
    {
        private CancellationTokenSource cancellationTokenSource;
        private CommandLineApplication application;
        private ILogger logger;
        private IMessageSubscriber subscriber;
        private IMigrationUseCase migrationService;

        public SubscriberApplication(
            CommandLineApplication application,
            CancellationTokenSource cancellationTokenSource,
            ILogger logger,
            IMessageSubscriber subscriber,
            IMigrationUseCase migrationService)
        {
            this.application = application;
            this.cancellationTokenSource = cancellationTokenSource;
            this.logger = logger;
            this.subscriber = subscriber;
            this.migrationService = migrationService;
        }

        public int Execute(string[] args)
        {
            this.application.Name = nameof(CleanSample);
            this.application.Description = "Messaging Subscriber";
            this.application.HelpOption("-h|--help");

            this.application.OnExecute(async() =>
            {
                return await this.ExecuteOnErrorHandleAsync("Subscriber", async() =>
                {
                    await this.migrationService.ExecuteAsync();
                    await this.subscriber.SubscribeAsync();
                    return 0;
                });
            });
            return this.application.Execute(args);
        }

        private async Task<int> ExecuteOnErrorHandleAsync(string commandName, Func<Task<int>> func)
        {
            try
            {
                this.logger?.LogInformation($"Start {commandName}");
                var res = await func();
                this.logger?.LogInformation($"End {commandName}");
                return res;
            }
            catch (OperationCanceledException ex)
            {
                this.logger?.LogWarning(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, $"Error {commandName}");
            }
            return 1;
        }
    }
}