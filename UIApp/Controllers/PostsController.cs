using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using UIApp.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UIApp.Models;

namespace UIApp.Controllers
{
    public partial class PostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        public PostsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("PostsApi");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel post, CancellationToken cancToken)
        {
            post.Tags = post.Tags![0].Split(',');

            var serializedPost = JsonConvert.SerializeObject(post);

            var content = new StringContent(serializedPost, encoding: null, "application/json");

            var token = await HttpContext.GetTokenAsync("access_token");

            _client.SetBearerToken(token!);

            var result = await _client.PostAsync("", content, cancToken);

            if (result.IsSuccessStatusCode)
            { 
                var jsonParsedResult = await result.Content.ReadFromJsonAsync<int>();
                return Redirect($"~/posts/showone/{jsonParsedResult}");
            }
            else
            {
                ModelState.AddModelError("", "Error while adding a new post");
                return View(new CreatePostViewModel());
            }
        }

        [HttpGet("posts/showone/{postId:int}")]
        public async Task<IActionResult> ShowOne(int postId, CancellationToken cancToken)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            _client.SetBearerToken(token!);

            var response = await _client.GetAsync($"{postId}", cancToken);

            if(response.IsSuccessStatusCode)
            {
                var post = await response.Content.ReadFromJsonAsync<PostDto>();
                return View(new ShowPostViewModel() { Post = post } );
            }
            else
            {
                ModelState.AddModelError("", "Error while getting post from api");
                return RedirectToAction("Index", "Home");
            }            
        }

        [HttpGet("posts/usersposts/{userId}")]
        public async Task<IActionResult> UsersPosts(int userId, CancellationToken cancToken)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            _client.SetBearerToken(token);

            var response = await _client.GetAsync($"users/{userId}", cancToken);

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadFromJsonAsync<PaginatedList<PostDto>>();
                return View(new ShowPostsViewModel() { Posts = posts });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
