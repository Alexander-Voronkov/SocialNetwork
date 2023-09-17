using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
