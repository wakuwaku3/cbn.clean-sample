using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.DDDSample.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "home-index";
        }
    }
}