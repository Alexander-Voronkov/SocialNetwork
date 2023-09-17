using Application.Reactions.Commands.CreateReaction;
using Application.Reactions.Commands.DeleteReaction;
using Application.Reactions.Commands.UpdateReaction;
using Application.Reactions.Queries;
using Application.Reactions.Queries.GetAllReactions;
using Application.Reactions.Queries.GetPostReactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {

        private readonly ISender _sender;
        public ReactionsController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<ReactionsController>
        [HttpGet("users/{userId}")]
        public async Task<IEnumerable<ReactionDto>> GetUsersReactions(GetAllUsersReactionsQuery query)
        {
            var reactions = await _sender.Send(query);

            return reactions;
        }

        // GET api/<ReactionsController>/5
        [HttpGet("posts/{postId}")]
        public async Task<IEnumerable<ReactionDto>> GetPostsReactions(GetPostReactionsQuery query)
        {
            var reactions = await _sender.Send(query);

            return reactions;
        }

        // POST api/<ReactionsController>
        [HttpPost]
        public async Task<int> CreateReaction(CreateReactionCommand command)
        {
            var createdReactionId = await _sender.Send(command);
            
            return createdReactionId;
        }

        // PUT api/<ReactionsController>/5
        [HttpPut]
        public async Task<int> UpdateReaction(UpdateReactionCommand command)
        {
            var updatedReactionId = await _sender.Send(command);

            return updatedReactionId;
        }

        // DELETE api/<ReactionsController>/5
        [HttpDelete]
        public async Task<int> DeleteReaction(DeleteReactionCommand command)
        {
            var deletedReactionId = await _sender.Send(command);

            return deletedReactionId;
        }
    }
}
