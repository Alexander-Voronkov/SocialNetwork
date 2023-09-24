using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UIApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 300)]
        public IActionResult Index(CancellationToken cancToken)
        {
            return View();
        }
    }
}
