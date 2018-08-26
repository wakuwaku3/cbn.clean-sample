using System;
using System.Threading.Tasks;
using Cbn.DDDSample.Domain.Account.Models;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.DDDSample.Subscriber.Executers
{
    public class WelcomeMailSender : IExecuter<WelcomeMailArgs>
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