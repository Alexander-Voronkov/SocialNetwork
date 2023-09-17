using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
