using System;
using System.Threading.Tasks;
using Cbn.DDDSample.Cli.Configuration;

namespace Cbn.DDDSample.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Startup().Execute(args);
        }
    }
}