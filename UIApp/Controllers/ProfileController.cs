using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UIApp.ViewModels;

namespace UIApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        
        public ProfileController(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
            _client = _httpClientFactory.CreateClient("UsersApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel viewModel)
        {
            return View();
        }
    }
}
