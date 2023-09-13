using Application.Chats.Queries.GetSingleChat;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries;
using Application.Posts.Queries.GetAllUsersPosts;
using Application.Posts.Queries.GetSignlePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ISender _sender;

        public PostsController(ISender sender)
        {
            _sender = sender;
        }


        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetUsersPosts(GetAllUsersPostsQuery query)
        {
            var posts = await _sender.Send(query);

            return posts;
        }

        // GET api/<PostsController>/5
        [HttpGet]
        public async Task<PostDto> GetPost(GetSinglePostQuery query)
        {
            var post = await _sender.Send(query);

            return post;
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<int> CreatePost(CreatePostCommand command)
        {
            var createdPostId = await _sender.Send(command);

            return createdPostId;
        }

        // PUT api/<PostsController>/5
        [HttpPut]
        public async Task<int> UpdatePost(UpdatePostCommand command)
        {
            var updatedPostId =  await _sender.Send(command);

            return updatedPostId;
        }

        // DELETE api/<PostsController>/5
        [HttpDelete]
        public async Task<int> Delete(DeletePostCommand command)
        {
            var deletedPostId = await _sender.Send(command);

            return deletedPostId;
        }
    }
}
