using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using UIApp.ViewModels;
using Newtonsoft.Json;
using UIApp.Models;
using UIApp.Services.Interfaces;
using AutoMapper;

namespace UIApp.Controllers
{
    public partial class PostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _postsClient;
        private readonly HttpClient _commentsClient;
        private readonly IUser _user;
        private readonly IMapper _mapper;
        public PostsController(IUser user, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _postsClient = _httpClientFactory.CreateClient("PostsApi");
            _commentsClient = _httpClientFactory.CreateClient("CommentsApi");
            _user = user;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel post, CancellationToken cancToken)
        {
            var serializedPost = JsonConvert.SerializeObject(post);

            var content = new StringContent(serializedPost, encoding: null, "application/json");

            var result = await _postsClient.PostAsync("", content, cancToken);

            if (result.IsSuccessStatusCode)
            { 
                var jsonParsedResult = await result.Content.ReadFromJsonAsync<int>();
                return RedirectToAction("ShowOne", "Posts", new { postId = jsonParsedResult });
            }
            else
            {
                ModelState.AddModelError("", "Error while adding a new post");
                return View(new CreatePostViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowOne(int postId, int? pageSize = 10, int? pageNumber = 1, CancellationToken cancToken = default)
        {
            var response = await _postsClient.GetAsync($"byid/{postId}", cancToken);

            if(response.IsSuccessStatusCode)
            {
                var post = await response.Content.ReadFromJsonAsync<PostDto>(cancellationToken: cancToken);
                
                var rawComments = await _commentsClient.GetAsync($"bypostid/{post!.Id}?pageSize={pageSize}&pageNumber={pageNumber}", cancToken);

                var comments = await rawComments.Content.ReadFromJsonAsync<PaginatedList<CommentDto>>(cancellationToken: cancToken);

                int likesCount = 0;
                int dislikesCount = 0;
                int angersCount = 0;
                int laughsCount = 0;
                int cryingsCount = 0;

                foreach (var reaction in post.Reactions!)
                {
                    if (reaction.Type == Enums.ReactionType.Like)
                        likesCount++;
                    else if (reaction.Type == Enums.ReactionType.Dislike)
                        dislikesCount++;
                    else if (reaction.Type == Enums.ReactionType.Laugh)
                        laughsCount++;
                    else if (reaction.Type == Enums.ReactionType.Anger)
                        angersCount++;
                    else if (reaction.Type == Enums.ReactionType.Crying)
                        cryingsCount++;
                }
                return View(new ShowPostViewModel()
                {
                    Post = post,
                    Comments = comments,
                    LikesCount = likesCount,
                    DislikesCount = dislikesCount,
                    AngersCount = angersCount,
                    CryingsCount = cryingsCount,
                    LaughsCount = laughsCount,
                });
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while getting post from api",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }            
        }

        [HttpGet]
        public async Task<IActionResult> UsersPosts(int? userId, int? pageNumber = 1, int? pageSize = 10, CancellationToken cancToken = default)
        {
            if (userId == null)
                userId = _user.Id;

            var response = await _postsClient.GetAsync($"byuserid/{userId}?pageNumber={pageNumber}&pageSize={pageSize}", cancToken);

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadFromJsonAsync<PaginatedList<PostDto>>();
                var mappedPosts = new PaginatedList<ShowPostViewModel>()
                {
                    PageNumber = posts!.PageNumber,
                    TotalCount = posts!.TotalCount,
                    TotalPages = posts!.TotalPages,
                };

                mappedPosts.Items = posts.Items.Select(x => 
                {
                    int likesCount = 0;
                    int dislikesCount = 0;
                    int angersCount = 0;
                    int laughsCount = 0;
                    int cryingsCount = 0;
                    foreach (var reaction in x.Reactions!)
                    {
                        if (reaction.Type == Enums.ReactionType.Like)
                            likesCount++;
                        else if (reaction.Type == Enums.ReactionType.Dislike)
                            dislikesCount++;
                        else if (reaction.Type == Enums.ReactionType.Laugh)
                            laughsCount++;
                        else if (reaction.Type == Enums.ReactionType.Anger)
                            angersCount++;
                        else if (reaction.Type == Enums.ReactionType.Crying)
                            cryingsCount++;
                    }
                    return new ShowPostViewModel()
                    {
                        Post = x,
                        LikesCount = likesCount,
                        DislikesCount = dislikesCount,
                        AngersCount = angersCount,
                        CryingsCount = cryingsCount,
                        LaughsCount = laughsCount,
                    };
                });

                return View(new ShowPostsViewModel() { Posts = mappedPosts });
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while getting user's posts",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel vm)
        {
            var serializedModel = JsonConvert.SerializeObject(vm);
            var result = await _commentsClient.PostAsync("", new StringContent(serializedModel, null, "application/json"));
            if(!result.IsSuccessStatusCode)
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while creating a comment",
                    ReturnUrl = Url.Action("Index", "Posts")
                });
            }
            return RedirectToAction("ShowOne", "Posts", new {postId = vm.PostId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int postId)
        {
            var res = await _postsClient.GetAsync($"byid/{postId}");

            if(res.IsSuccessStatusCode)
            {
                var post = res.Content.ReadFromJsonAsync<PostDto>();
                var viewModel = _mapper.Map<EditPostViewModel>(post);
                return View(viewModel);
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Error while getting a post",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel vm, CancellationToken cancToken)
        {
            if(ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm), null, "application/json");
                var res = await _postsClient.PutAsync("", content, cancToken);
                if(res.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowOne", "Posts", new { postId = await res.Content.ReadFromJsonAsync<int>() });
                }
                else
                {
                    return View("MyError", new MyErrorViewModel()
                    {
                        Message = "Error while updating post",
                        ReturnUrl = Url.Action("Index", "Home")
                    });
                }
            }
            else
            {
                return View("MyError", new MyErrorViewModel()
                {
                    Message = "Wrong data entered while editing post",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }
        }
    }
}
