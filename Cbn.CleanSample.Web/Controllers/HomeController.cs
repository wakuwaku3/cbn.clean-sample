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