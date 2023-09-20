using Application.Chats.Queries.GetSingleChat;
using Application.Common.Models;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries;
using Application.Posts.Queries.GetAllUsersPosts;
using Application.Posts.Queries.GetSignlePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet("byuserid/{userId:int}")]
        public async Task<PaginatedList<PostDto>> GetUsersPosts([FromRoute]GetAllUsersPostsQuery query)
        {
            var posts = await _sender.Send(query);

            return posts;
        }

        [HttpGet("byid/{postId:int}")]
        public async Task<PostDto> GetPost([FromRoute]GetSinglePostQuery query)
        {
            var post = await _sender.Send(query);

            return post;
        }

        [HttpPost]
        public async Task<int> CreatePost(CreatePostCommand command)
        {
            var createdPostId = await _sender.Send(command);

            return createdPostId;
        }

        [HttpPut]
        public async Task<int> UpdatePost(UpdatePostCommand command)
        {
            var updatedPostId =  await _sender.Send(command);

            return updatedPostId;
        }

        [HttpDelete]
        public async Task<int> Delete(DeletePostCommand command)
        {
            var deletedPostId = await _sender.Send(command);

            return deletedPostId;
        }
    }
}
