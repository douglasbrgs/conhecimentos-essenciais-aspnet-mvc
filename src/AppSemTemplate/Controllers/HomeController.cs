using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return View();
        }
    }
}
