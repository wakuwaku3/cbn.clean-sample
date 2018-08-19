using System;
using System.Diagnostics;
using Cbn.DDDSample.Application;
using Cbn.DDDSample.Application.Services;
using Cbn.DDDSample.Web.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.DDDSample.Web.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService homeService;

        public HomeController(IHomeService homeService) : base()
        {
            this.homeService = homeService;
        }
        public string Index()
        {
            return this.homeService.Execute();
        }
    }
}