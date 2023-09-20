using Application.Common.Models;
using Application.Friendrequests.Commands.CreateFriendrequest;
using Application.Friendrequests.Commands.DeleteFriendrequest;
using Application.Friendrequests.Queries;
using Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received;
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

        [HttpGet("received/byuserid/{userId:int}")]
        public async Task<PaginatedList<FriendrequestDto>> GetReceivedFriendrequests([FromRoute] GetAllUsersReceivedFriendrequestsQuery query)
        {
            var requests = await _sender.Send(query);

            return requests;
        }

        [HttpGet("sent/byuserid/{userId:int}")]
        public async Task<PaginatedList<FriendrequestDto>> GetSentFriendrequests([FromRoute] GetAllUsersSentFriendrequestsQuery query)
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

        [HttpDelete]
        public async Task<int> DeleteFriendrequest(DeleteFriendrequestCommand command)
        {
            var deletedFriendrequestId = await _sender.Send(command);

            return deletedFriendrequestId;
        }
    }
}
