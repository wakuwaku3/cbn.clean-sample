using Cbn.CleanSample.Cli.Configuration;

namespace Cbn.CleanSample.Messaging.Receiver
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Startup(args).Execute();
        }
    }
}