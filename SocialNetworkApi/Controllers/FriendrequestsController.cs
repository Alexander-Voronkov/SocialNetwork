using Application.Common.Models;
using Application.Friendships.Commands.CreateFriendship;
using Application.Friendships.Commands.DeleteFriendship.ById;
using Application.Friendships.Queries;
using Application.Friendships.Queries.GetUsersFriendrequests.Received;
using Application.Friendships.Queries.GetUsersFriendrequests.Sent;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendrequestsController : ControllerBase
    {

        private readonly ISender _sender;
        public FriendrequestsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("received/byuserid")]
        public async Task<PaginatedList<FriendshipDto>> GetReceivedFriendrequests([FromQuery] GetUsersReceivedFriendrequestsQuery query)
        {
            var requests = await _sender.Send(query);

            return requests;
        }

        [HttpGet("sent/byuserid")]
        public async Task<PaginatedList<FriendshipDto>> GetSentFriendrequests([FromQuery] GetUsersSentFriendrequestsQuery query)
        {
            var requests = await _sender.Send(query);

            return requests;
        }

        [HttpPost]
        public async Task<int> SendFriendRequest(CreateFriendrequestCommand command)
        {
            var createdFriendrequestId = await _sender.Send(command);

            return createdFriendrequestId;
        }

        [HttpDelete("{FriendshipId:int}")]
        public async Task<int> DeleteFriendrequest([FromRoute] DeleteFriendshipByIdCommand command)
        {
            var deletedFriendshipId = await _sender.Send(command);

            return deletedFriendshipId;
        }
    }
}
