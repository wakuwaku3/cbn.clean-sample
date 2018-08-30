using System;
using System.Threading.Tasks;
using Cbn.CleanSample.Cli.Configuration;

namespace Cbn.CleanSample.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Startup(args).Execute();
        }
    }
}