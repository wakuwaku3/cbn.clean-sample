using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Cbn.CleanSample.Messaging.Subscriber.Configuration;
using Cbn.Infrastructure.Autofac.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cbn.CleanSample.Cli.Configuration
{
    public class Startup
    {
        private string[] args;
        private ILoggerFactory loggerFactory;
        private IConfigurationRoot configurationRoot;
        private Assembly executeAssembly;
        private string rootPath;

        public Startup(string[] args)
        {
            this.args = args;
            this.loggerFactory = new LoggerFactory();
            this.loggerFactory.AddConsole();
            this.loggerFactory.AddDebug();
            this.executeAssembly = Assembly.GetEntryAssembly();
            this.rootPath = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
                .AddJsonFile($"appsettings.user.json", optional : true)
                .AddUserSecrets(this.executeAssembly)
                .AddEnvironmentVariables()
                .AddCommandLine(this.args);

            this.configurationRoot = configurationBuilder.Build();
        }

        /// <summary>
        /// 実行
        /// </summary>
        public int Execute()
        {
            var builder = new AutofacBuilder();
            builder.RegisterModule(new SubscriberDIModule(this.executeAssembly, this.rootPath, this.configurationRoot, this.loggerFactory));
            using(var scope = builder.Build())
            {
                var cts = scope.Resolve<CancellationTokenSource>();
                var action = new Action<AssemblyLoadContext>(context =>
                {
                    cts.Cancel(false);
                });
                AssemblyLoadContext.Default.Unloading += action;
                try
                {
                    var app = scope.Resolve<SubscriberApplication>();
                    return app.Execute(args);
                }
                finally
                {
                    AssemblyLoadContext.Default.Unloading -= action;
                }
            }
        }
    }
}