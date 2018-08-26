using Cbn.DDDSample.Cli.Configuration;

namespace Cbn.DDDSample.Subscriber
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Startup(args).Execute();
        }
    }
}