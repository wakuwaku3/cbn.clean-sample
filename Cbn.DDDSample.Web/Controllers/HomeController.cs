using System;
using System.Diagnostics;
using Cbn.DDDSample.Application;
using Cbn.DDDSample.Application.Services;
using Cbn.DDDSample.Application.Services.Interfaces;
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