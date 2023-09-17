using Application.Messages.Commands.CreateMessage;
using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.UpdateMessage;
using Application.Messages.Queries;
using Application.Messages.Queries.GetAllUsersMessages;
using Application.Messages.Queries.GetChatsMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly ISender _sender;
        public MessagesController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<MessagesController>
        [HttpGet("users/{userId}")]
        public async Task<IEnumerable<MessageDto>> GetAllUsersMessages(GetAllUsersMessagesQuery query)
        {
            var messages = await _sender.Send(query);

            return messages;
        }

        // GET api/<MessagesController>/5
        [HttpGet("chats/{chatId}")]
        public async Task<IEnumerable<MessageDto>> GetChatsMessages(GetChatsMessagesQuery query)
        {
            var messages = await _sender.Send(query);

            return messages;
        }

        // POST api/<MessagesController>
        [HttpPost]
        public async Task<int> WriteMessage(CreateMessageCommand command)
        {
            var writtedMessageId = await _sender.Send(command);

            return writtedMessageId;
        }

        // PUT api/<MessagesController>/5
        [HttpPut]
        public async Task<int> EditMessage(UpdateMessageCommand command)
        {
            var updatedMessageId = await _sender.Send(command);

            return updatedMessageId;
        }

        // DELETE api/<MessagesController>/5
        [HttpDelete]
        public async Task<int> DeleteMessage(DeleteMessageCommand command)
        {
            var deletedMessageId = await _sender.Send(command);

            return deletedMessageId;
        }
    }
}
