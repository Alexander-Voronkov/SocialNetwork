using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
