using Application.Friendships.Commands.CreateFriendship;
using Application.Friendships.Commands.DeleteFriendship;
using Application.Friendships.Queries;
using Application.Friendships.Queries.GetAllUsersFriendships;
using Application.Friendships.Queries.GetSingleFriendship;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<FriendshipsController>
        [HttpGet("users/{userId}")]
        public async Task<IEnumerable<FriendshipDto>> GetUsersFriendships(GetAllUsersFriendshipsQuery query)
        {
            var friendships = await _sender.Send(query);

            return friendships;
        }

        // GET api/<FriendshipsController>/5
        [HttpGet("{friendshipId}")]
        public async Task<FriendshipDto> Get(GetSingleFriendshipQuery query)
        {
            var friendship = await _sender.Send(query);

            return friendship;
        }

        // POST api/<FriendshipsController>
        [HttpPost]
        public async Task<int> AcceptFriendrequest(CreateFriendshipCommand command)
        {
            var createdFriendshipId = await _sender.Send(command);

            return createdFriendshipId;
        }

        // DELETE api/<FriendshipsController>/5
        [HttpDelete]
        public async Task<int> DeleteFriendship(DeleteFriendshipCommand command)
        {
            var deletedFriendshipId = await _sender.Send(command);

            return deletedFriendshipId;
        }
    }
}
