using Application.Chats.Commands.CreateChat;
using Application.Chats.Commands.DeleteChat;
using Application.Chats.Queries;
using Application.Chats.Queries.GetUsersChats;
using Application.Common.Interfaces;
using Application.Messages.Queries.GetChatsMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IUser _user;
        // GET: api/<ChatsController>

        public ChatsController(ISender sender, IUser user)
        {
            _sender = sender;
            _user = user;
        }

        // GET api/<ChatsController>/5
        [HttpGet]
        public async Task<IEnumerable<ChatDto>> Get(GetUsersChatsQuery query)
        {
            var chats = await _sender.Send(query);

            return chats;
        }

        // POST api/<ChatsController>
        [HttpPost]
        public async Task<int> Post(CreateChatCommand command)
        {
            var createdChatId = await _sender.Send(command);

            return createdChatId;
        }

        // DELETE api/<ChatsController>/5
        [HttpDelete]
        public async Task<int> Delete(DeleteChatCommand command)
        {
            var deletedChatId = await _sender.Send(command);

            return deletedChatId;
        }
    }
}
