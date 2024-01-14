using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UIApp.Models;
using UIApp.Services.Interfaces;
using UIApp.ViewModels;

namespace UIApp.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IUser _user;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _friendsClient;
        private readonly HttpClient _friendrequestClient;
        public FriendsController(IUser user, IHttpClientFactory clientFactory) 
        {
            _user = user;
            _httpClientFactory = clientFactory;
            _friendsClient = _httpClientFactory.CreateClient("FriendshipsApi");
            _friendrequestClient = _httpClientFactory.CreateClient("FriendrequestsApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? userId, int? pageNumber = 1, int? pageSize = 10)
        {
            if (userId == null)
                userId = _user.Id;
            var res = await _friendsClient.GetAsync($"byuserid/{userId}?pageNumber={pageNumber}&pageSize={pageSize}");
            if (res.IsSuccessStatusCode)
            {
                var friends = await res.Content.ReadFromJsonAsync<PaginatedList<FriendshipDto>>();
                var viewModel = new MyFriendsViewModel()
                {
                    Friends = friends
                };
                return View(viewModel);
            }
            return View("MyError", new MyErrorViewModel()
            {
                ReturnUrl = Url.Action("Index", "Home"),
                Message = "An error ocurred while getting friends"
            });
        }

        [HttpGet]
        public async Task<IActionResult> ReceivedFriendrequests(int? pageNumber = 1, int? pageSize = 10)
        {
            var res = await _friendrequestClient.GetAsync($"received/byuserid?UserId={_user.Id}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (res.IsSuccessStatusCode)
            {
                var requests = await res.Content.ReadFromJsonAsync<PaginatedList<FriendshipDto>>();
                var viewModel = new MyFriendrequestsViewModel()
                {
                    Friendrequests = requests
                };
                return View(viewModel);
            }
            return View("MyError", new MyErrorViewModel()
            {
                Message = "An error ocurred while getting received friendrequests.",
                ReturnUrl = Url.Action("Index", "Home")
            });
        }

        [HttpGet]
        public async Task<IActionResult> Friendrequests()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SentFriendrequests(int? pageNumber = 1, int? pageSize = 10)
        {
            var res = await _friendrequestClient.GetAsync($"sent/byuserid?UserId={_user.Id}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (res.IsSuccessStatusCode)
            {
                var requests = await res.Content.ReadFromJsonAsync<PaginatedList<FriendshipDto>>();
                var viewModel = new MyFriendrequestsViewModel()
                {
                    Friendrequests = requests
                };
                return View(viewModel);
            }
            return View("MyError", new MyErrorViewModel()
            {
                ReturnUrl = Url.Action("Index", "Home"),
                Message = "An error ocurred while getting sent friendrequests"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CancelFriendrequest(int? friendRequestId, CancellationToken cancToken)
        {
            var res = await _friendrequestClient.DeleteAsync($"{friendRequestId}", cancToken);
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Friends");
            }
            return View("MyError", new MyErrorViewModel()
            {
                Message = "An error ocurred while cancelling the friendrequest",
                ReturnUrl = Url.Action("Index", "Home")
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(int? userId, CancellationToken cancellationToken)
        {
            if(userId == null)
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Invalid arguments",
                    ReturnUrl = Url.Action("Index", "Friends")
                });
            }
            else
            {
                var res = await _friendsClient.DeleteAsync($"byuserid/{userId}", cancellationToken);
                if(res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Friends");
                }
                else
                {
                    return View("MyError", new MyErrorViewModel()
                    {
                        Message = "Error while removing friend",
                        ReturnUrl = Url.Action("Index", "Friends")
                    });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddFriends(string? searchName, int? pageNumber = 1, int? pageSize = 10, CancellationToken cancToken = default)
        {
            if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrWhiteSpace(searchName))
            {
                var client = _httpClientFactory.CreateClient("UsersApi");

                var response = await client.GetAsync($"byusername/{searchName}?pageNumber={pageNumber}&pageSize={pageSize}", cancToken);

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<PaginatedList<UserDto>>(cancellationToken: cancToken);
                    return View(new AddFriendViewModel() { Users = users, SearchName = searchName });
                }
                else
                {
                    return View("MyError", new MyErrorViewModel()
                    {
                        Message = "Error while searching for users",
                        ReturnUrl = Url.Action("Index", "Home")
                    });
                }
            }
            return View(new AddFriendViewModel() { });
        }

        [HttpPost]
        public async Task<IActionResult> AddFriends(int? userId, CancellationToken cancToken)
        {
            if(userId == null)
            {
                ModelState.AddModelError("", "User id is required");
                return View();
            }

            var json = JsonConvert.SerializeObject(new { SecondUserId = userId.ToString() });

            var content = new StringContent(json, null, "application/json");

            var res = await _friendrequestClient.PostAsync("", content, cancToken);
        
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("SentFriendrequests", "Friends");
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "An error ocurred while sending a friendrequest",
                    ReturnUrl = Url.Action("SentFriendrequests", "Friends")
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriendrequest(int? friendRequestId, CancellationToken cancToken)
        {
            if(friendRequestId == null)
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Wrong user id",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }

            var content = new StringContent(JsonConvert.SerializeObject(new { FriendshipId = friendRequestId }), null, "application/json");

            var res = await _friendsClient.PostAsync("", content, cancToken);
        
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Friends");
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "An error ocurred while acception a friendrequest",
                    ReturnUrl = Url.Action("Index", "Friends")
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? userId)
        {
            if (userId == null)
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Wrong friend id to delete",
                    ReturnUrl = Url.Action("Index", "Home")
                });

            var res = await _friendsClient.DeleteAsync($"byuserid/{userId}");
            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while deleting friend",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }
        }
    }
}
