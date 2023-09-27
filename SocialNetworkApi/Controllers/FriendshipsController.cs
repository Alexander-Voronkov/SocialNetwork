using Application.Common.Models;
using Application.Friendships.Commands.AcceptFriendship;
using Application.Friendships.Commands.DeleteFriendship.ById;
using Application.Friendships.Commands.DeleteFriendship.ByUserId;
using Application.Friendships.Queries;
using Application.Friendships.Queries.GetSingleFriendship;
using Application.Friendships.Queries.GetUsersFriendships;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {

        private readonly ISender _sender;
        public FriendshipsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("byuserid/{UserId:int}")]
        public async Task<PaginatedList<FriendshipDto>> GetUsersFriendships([FromRoute] GetUsersFriendshipsQuery query)
        {
            var friendships = await _sender.Send(query);

            return friendships;
        }

        [HttpGet("byid/{FriendshipId:int}")]
        public async Task<FriendshipDto> Get([FromRoute] GetSingleFriendshipQuery query)
        {
            var friendship = await _sender.Send(query);

            return friendship;
        }

        [HttpPost]
        public async Task<int> AcceptFriendrequest(AcceptFriendshipCommand command)
        {
            var createdFriendshipId = await _sender.Send(command);

            return createdFriendshipId;
        }

        [HttpDelete("byid/{FriendshipId:int}")]
        public async Task<int> DeleteFriendship([FromRoute] DeleteFriendshipByIdCommand command)
        {
            var deletedFriendshipId = await _sender.Send(command);

            return deletedFriendshipId;
        }

        [HttpDelete("byuserid/{UserId:int}")]
        public async Task<int> DeleteFriendship([FromRoute] DeleteFriendshipByUserIdCommand command)
        {
            var deletedFriendshipId = await _sender.Send(command);

            return deletedFriendshipId;
        }
    }
}
