using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using UIApp.ViewModels;
using Newtonsoft.Json;
using UIApp.Models;
using UIApp.Services.Interfaces;

namespace UIApp.Controllers
{
    public partial class PostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _postsClient;
        private readonly HttpClient _commentsClient;
        private readonly IUser _user;
        public PostsController(IUser user, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _postsClient = _httpClientFactory.CreateClient("PostsApi");
            _commentsClient = _httpClientFactory.CreateClient("CommentsApi");
            
            _user = user;
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
            var response = await _postsClient.GetAsync($"byid/{postId}", cancToken);

            if(response.IsSuccessStatusCode)
            {
                var post = await response.Content.ReadFromJsonAsync<PostDto>();
                var rawComments = await _commentsClient.GetAsync($"bypostid/{post!.Id}");

                var comments = await rawComments.Content.ReadFromJsonAsync<PaginatedList<CommentDto>>();

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
                ModelState.AddModelError("", "Error while getting post from api");
                return RedirectToAction("Index", "Home");
            }            
        }

        [HttpGet("posts/usersposts/{userId:int?}")]
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
                    foreach (var reaction in x.Reactions)
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
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel vm)
        {
            var serializedModel = JsonConvert.SerializeObject(vm);
            var result = await _commentsClient.PostAsync("", new StringContent(serializedModel, null, "application/json"));
            if(!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error while creating a comment");
            }
            return View();
        }

        // todo

        [HttpGet("posts/edit/{postId:int}")]
        public async Task<IActionResult> Edit(int postId)
        {
            return View(new EditPostViewModel());
        }

        [HttpPost("posts/edit/{postId:int}")]
        public async Task<IActionResult> Edit(EditPostViewModel vm)
        {
            return View(new EditPostViewModel());
        }
    }
}
