using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries;
using Application.Comments.Queries.GetAllPostComments;
using Application.Comments.Queries.GetAllUsersComments;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ISender _sender;

        public CommentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("bypostid/{postId:int}")]
        public async Task<PaginatedList<CommentDto>> GetPostsComments([FromRoute] GetAllPostCommentsQuery query)
        {
            var comments = await _sender.Send(query);

            return comments;
        }

        [HttpGet("byuserid/{userId:int}")]
        public async Task<PaginatedList<CommentDto>> GetUsersComments([FromRoute] GetAllUsersCommentsQuery query)
        {
            var comments = await _sender.Send(query);

            return comments;
        }

        [HttpPost]
        public async Task<int> WriteComment(CreateCommentCommand command)
        {
            var createdCommentId = await _sender.Send(command);

            return createdCommentId;
        }

        [HttpPut]
        public async Task<int> EditComment(UpdateCommentCommand command)
        {
            var updatedCommentId = await _sender.Send(command);

            return updatedCommentId;
        }

        [HttpDelete]
        public async Task<int> DeleteComment(DeleteCommentCommand command)
        {
            var deletedCommentId = await _sender.Send(command);

            return deletedCommentId;
        }
    }
}
