using System;
using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.CleanSample.Messaging.Receiver.Receivers
{
    public class WelcomeMailSender : IMessageReceiver<WelcomeMailArgs>
    {
        public WelcomeMailSender(WelcomeMailArgs parameter)
        {
            this.Parameter = parameter;
        }
        public WelcomeMailArgs Parameter { get; }

        public Task<int> ExecuteAsync()
        {
            Console.WriteLine($"{this.Parameter.Email} {this.Parameter.Name}");
            return Task.FromResult(0);
        }
    }
}