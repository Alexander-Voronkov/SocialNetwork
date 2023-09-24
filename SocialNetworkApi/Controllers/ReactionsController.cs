using Application.Common.Models;
using Application.Reactions.Commands.CreateReaction;
using Application.Reactions.Commands.DeleteReaction;
using Application.Reactions.Queries;
using Application.Reactions.Queries.GetPostReactions;
using Application.Reactions.Queries.GetUsersReactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet("byuserid/{userId:int}")]
        public async Task<PaginatedList<ReactionDto>> GetUsersReactions([FromRoute] GetUsersReactionsQuery query)
        {
            var reactions = await _sender.Send(query);

            return reactions;
        }

        [HttpGet("bypostid/{postId:int}")]
        public async Task<PaginatedList<ReactionDto>> GetPostsReactions([FromRoute] GetPostReactionsQuery query)
        {
            var reactions = await _sender.Send(query);

            return reactions;
        }

        [HttpPost]
        public async Task<int> CreateReaction(CreateReactionCommand command)
        {
            var createdReactionId = await _sender.Send(command);
            
            return createdReactionId;
        }

        [HttpDelete("{ReactionId:int}")]
        public async Task<int> DeleteReaction([FromRoute] DeleteReactionCommand command)
        {
            var deletedReactionId = await _sender.Send(command);

            return deletedReactionId;
        }
    }
}
