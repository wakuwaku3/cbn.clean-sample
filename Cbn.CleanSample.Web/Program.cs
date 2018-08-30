using System.IO;
using Cbn.CleanSample.Web.Configuration;
using Cbn.Infrastructure.Autofac.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Cbn.CleanSample.Web
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