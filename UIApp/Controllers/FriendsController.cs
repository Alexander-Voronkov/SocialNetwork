using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
