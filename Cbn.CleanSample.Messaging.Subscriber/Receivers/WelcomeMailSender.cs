using System;
using System.Threading;
using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.CleanSample.Messaging.Subscriber.Receivers
{
    public class WelcomeMailSender : IMessageReceiver<WelcomeMailArgs>
    {
        private WelcomeMailArgs args;
        private CancellationTokenSource source;

        public WelcomeMailSender(
            CancellationTokenSource source,
            WelcomeMailArgs args)
        {
            this.source = source;
            this.args = args;
        }

        public async Task<int> ExecuteAsync()
        {
            Console.WriteLine($"{this.args.Email} {this.args.Name}");
            return await Task.FromResult(0);
        }
    }
}