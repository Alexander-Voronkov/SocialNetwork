using AutoMapper;
using Data.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UIApp.Services.Interfaces;
using UIApp.ViewModels;

namespace UIApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly IUser _user;
        private readonly IMapper _mapper;
        
        public ProfileController(IMapper mapper, IUser user, IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
            _client = _httpClientFactory.CreateClient("UsersApi");
            _user = user;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancToken)
        {             
            var response = await _client.GetAsync($"byid/{_user.Id}", cancToken);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken: cancToken);

                var vm = _mapper.Map<ProfileViewModel>(user);

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string oldPassword, string newPassword, CancellationToken cancToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsername(string newUsername, CancellationToken cancToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail(string newEmail, CancellationToken cancToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhone(string newPhone, CancellationToken cancToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut(CancellationToken cancToken)
        {
            return SignOut(
                new AuthenticationProperties(),
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(CancellationToken cancToken)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                await HttpContext.ChallengeAsync();
                return new EmptyResult(); 
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
