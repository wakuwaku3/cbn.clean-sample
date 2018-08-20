using System.IO;
using Cbn.DDDSample.Web.Configuration;
using Cbn.Infrastructure.Autofac.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Cbn.DDDSample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddDI())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}