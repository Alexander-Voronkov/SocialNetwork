using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries;
using Application.Comments.Queries.GetAllPostComments;
using Application.Comments.Queries.GetAllUsersComments;
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


        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetPostsComments(GetAllPostCommentsQuery query)
        {
            var comments = await _sender.Send(query);

            return comments;
        }

        // GET api/<CommentsController>/5
        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetUsersComments(GetAllUsersCommentsQuery query)
        {
            var comments = await _sender.Send(query);

            return comments;
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<int> WriteComment(CreateCommentCommand command)
        {
            var createdCommentId = await _sender.Send(command);

            return createdCommentId;
        }

        // PUT api/<CommentsController>/5
        [HttpPut]
        public async Task<int> EditComment(UpdateCommentCommand command)
        {
            var updatedCommentId = await _sender.Send(command);

            return updatedCommentId;
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete]
        public async Task<int> DeleteComment(DeleteCommentCommand command)
        {
            var deletedCommentId = await _sender.Send(command);

            return deletedCommentId;
        }
    }
}
