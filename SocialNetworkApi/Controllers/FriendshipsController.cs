using Application.Common.Models;
using Application.Friendships.Commands.CreateFriendship;
using Application.Friendships.Commands.DeleteFriendship;
using Application.Friendships.Queries;
using Application.Friendships.Queries.GetAllUsersFriendships;
using Application.Friendships.Queries.GetSingleFriendship;
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

        [HttpGet("byuserid/{userId:int}")]
        public async Task<PaginatedList<FriendshipDto>> GetUsersFriendships([FromRoute] GetAllUsersFriendshipsQuery query)
        {
            var friendships = await _sender.Send(query);

            return friendships;
        }

        [HttpGet("byid/{friendshipId:int}")]
        public async Task<FriendshipDto> Get([FromRoute] GetSingleFriendshipQuery query)
        {
            var friendship = await _sender.Send(query);

            return friendship;
        }

        [HttpPost]
        public async Task<int> AcceptFriendrequest(CreateFriendshipCommand command)
        {
            var createdFriendshipId = await _sender.Send(command);

            return createdFriendshipId;
        }

        [HttpDelete("{FriendshipId:int}")]
        public async Task<int> DeleteFriendship([FromRoute] DeleteFriendshipCommand command)
        {
            var deletedFriendshipId = await _sender.Send(command);

            return deletedFriendshipId;
        }
    }
}
