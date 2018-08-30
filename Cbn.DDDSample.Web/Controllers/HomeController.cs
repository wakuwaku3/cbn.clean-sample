using System;
using System.Diagnostics;
using Cbn.DDDSample.UseCases;
using Cbn.DDDSample.UseCases.Interfaces.Services;
using Cbn.DDDSample.UseCases.Services;
using Cbn.DDDSample.Web.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.DDDSample.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "welcome ddd sample";
        }
    }
}