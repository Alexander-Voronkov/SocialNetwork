using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
