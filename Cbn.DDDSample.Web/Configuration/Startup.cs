using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cbn.DDDSample.Web.Configuration.Interfaces;
using Cbn.Infrastructure.AspNetCore.Extensions;
using Cbn.Infrastructure.AspNetCore.Middlewares.Extensions;
using Cbn.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cbn.DDDSample.Web.Configuration
{
    public class Startup
    {
        private IHostingEnvironment hostingEnvironment;
        private ILoggerFactory loggerFactory;
        private IConfiguration configuration;
        private IConfigurationRoot configurationRoot;
        private Assembly executeAssembly;
        private string rootPath;
        private IDDDSampleWebConfig config;
        private IContainer container;

        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.loggerFactory = loggerFactory;
            this.configuration = configuration;
            this.loggerFactory.AddConsole(this.configuration.GetSection("Logging"));
            this.loggerFactory.AddDebug();
            this.executeAssembly = Assembly.GetEntryAssembly();
            this.rootPath = Directory.GetCurrentDirectory();
            this.configurationRoot = this.CreateConfigurationRoot();
        }

        private IConfigurationRoot CreateConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional : true);
            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets(this.executeAssembly);
            }
            builder.AddEnvironmentVariables();
            return builder.Build();
        }

        private void CreateContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new DDDSampleAutofacModule(this.executeAssembly, this.rootPath, this.configurationRoot, this.loggerFactory));
            this.container = builder.Build();
            this.config = this.container.Resolve<IDDDSampleWebConfig>();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            this.CreateContainer(services);
            services.AddWebApiService(this.config);

            return new AutofacServiceProvider(this.container);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebApiServiceMiddlewares(this.config);
        }
    }
}