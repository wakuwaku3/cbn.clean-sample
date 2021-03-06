﻿using System;
using System.IO;
using System.Reflection;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.CleanSample.UseCases.Migration;
using Cbn.Infrastructure.AspNetCore.Extensions;
using Cbn.Infrastructure.AspNetCore.Middlewares.Extensions;
using Cbn.Infrastructure.Autofac.Builder;
using Cbn.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cbn.CleanSample.Web.Configuration
{
    public class Startup
    {
        private IHostingEnvironment hostingEnvironment;
        private ILoggerFactory loggerFactory;
        private IConfiguration configuration;
        private IConfigurationRoot configurationRoot;
        private Assembly executeAssembly;
        private string rootPath;
        private CleanSampleWebConfig config;

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
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional : true)
                .AddJsonFile($"appsettings.user.json", optional : true)
                .AddEnvironmentVariables();
            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets(this.executeAssembly);
            }
            builder.AddEnvironmentVariables();
            return builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            this.config = new CleanSampleWebConfig(this.configurationRoot);
            services.AddWebApiService(this.config);
            services.AddMvc().AddControllersAsServices();

            var builder = new AutofacBuilder();
            builder.Populate(services);
            builder.RegisterModule(new CleanSampleWebDIModule(this.executeAssembly, this.rootPath, this.configurationRoot, this.loggerFactory));
            var scope = builder.Build();
            var migrationService = scope.Resolve<IMigrationUseCase>();
            migrationService.ExecuteAsync().GetAwaiter().GetResult();
            return builder.CreateServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseWebApiServiceMiddlewares<UserClaim>(this.config);
        }
    }
}