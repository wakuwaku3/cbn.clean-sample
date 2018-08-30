using System;
using System.Diagnostics;
using Cbn.CleanSample.UseCases;
using Cbn.CleanSample.UseCases.Interfaces.Services;
using Cbn.CleanSample.UseCases.Services;
using Cbn.CleanSample.Web.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.CleanSample.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "welcome clean sample";
        }
    }
}