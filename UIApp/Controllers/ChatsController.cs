using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UIApp.Models;
using UIApp.Services.Interfaces;
using UIApp.ViewModels;

namespace UIApp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly IUser _user;
        public ChatsController(IUser user, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("ChatsApi");
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> MyChats(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancToken = default)
        {
            var response = await _client.GetAsync($"byuserid/{_user.Id!}?pageNumber={pageNumber}&pageSize={pageSize}", cancToken);

            if(response.IsSuccessStatusCode)
            {
                var chats = await response.Content.ReadFromJsonAsync<PaginatedList<ChatDto>>(cancellationToken : cancToken);
                return View(new MyChatsViewModel() { Chats = chats });
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    ReturnUrl = Url.Action("Index", "Home"),
                    Message = "Error while getting chats"
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SingleChat(int? chatId, CancellationToken cancToken)
        {
            var response = await _client.GetAsync($"byid/{chatId}", cancToken);

            if(response.IsSuccessStatusCode)
            {
                var chat = await response.Content.ReadFromJsonAsync<ChatDto>(cancellationToken:cancToken);
                
                return View(new SingleChatViewModel() { Chat = chat });
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while getting chat",
                    ReturnUrl = Url.Action("MyChats", "Chats")
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateChat(string? searchName, int? pageNumber = 1, int? pageSize = 10, CancellationToken cancToken = default)
        {
            if(!string.IsNullOrEmpty(searchName) && !string.IsNullOrWhiteSpace(searchName))
            {
                var client = _httpClientFactory.CreateClient("UsersApi");

                var response = await client.GetAsync($"byusername/{searchName}?pageNumber={pageNumber}&pageSize={pageSize}", cancToken);

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserDto>>(cancellationToken : cancToken);
                    return View(new CreateChatViewModel() { Users = users });
                }
                else
                {
                    return View("MyError", new MyErrorViewModel()
                    {
                        Message = "Error while creating a chat",
                        ReturnUrl = Url.Action("Index", "Home")
                    });
                }
            }            
            return View(new CreateChatViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatViewModel viewModel, CancellationToken cancToken)
        {
            var serializedChat = JsonConvert.SerializeObject(new ChatDto() { SecondUserId = viewModel.SecondUserId });

            var content = new StringContent(serializedChat, null, "application/json");

            var response = await _client.PostAsync("", content, cancToken);

            if(response.IsSuccessStatusCode) 
            {
                var chatId = await response.Content.ReadFromJsonAsync<int>(cancellationToken : cancToken);
                return RedirectToAction("SingleChat", chatId);
            }
            else
            {
                ModelState.AddModelError("", "Error while creating a chat");
                return View(new CreateChatViewModel());
            }
        }
    }
}
