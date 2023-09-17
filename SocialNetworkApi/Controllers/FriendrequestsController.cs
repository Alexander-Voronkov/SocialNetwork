using Application.Friendrequests.Commands.CreateFriendrequest;
using Application.Friendrequests.Commands.DeleteFriendrequest;
using Application.Friendrequests.Queries;
using Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<FriendrequestsController>
        [HttpGet("received/{userId}")]
        public async Task<IEnumerable<FriendrequestDto>> GetReceivedFriendrequests(GetAllUsersReceivedFriendrequestsQuery query)
        {
            var requests = await _sender.Send(query);

            return requests;
        }

        // GET: api/<FriendrequestsController>
        [HttpGet("sent/{userId}")]
        public async Task<IEnumerable<FriendrequestDto>> GetSentFriendrequests(GetAllUsersSentFriendrequestsQuery query)
        {
            var requests = await _sender.Send(query);

            return requests;
        }

        // POST api/<FriendrequestsController>
        [HttpPost]
        public async Task<int> SendFriendRequest(CreateFriendrequestCommand command)
        {
            var createdFriendrequestId = await _sender.Send(command);

            return createdFriendrequestId;
        }

        // DELETE api/<FriendrequestsController>/5
        [HttpDelete]
        public async Task<int> DeleteFriendrequest(DeleteFriendrequestCommand command)
        {
            var deletedFriendrequestId = await _sender.Send(command);

            return deletedFriendrequestId;
        }
    }
}
