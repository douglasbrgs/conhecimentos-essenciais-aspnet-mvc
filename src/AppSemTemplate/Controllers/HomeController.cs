using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
