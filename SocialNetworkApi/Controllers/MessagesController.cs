using Application.Common.Models;
using Application.Messages.Commands.CreateMessage;
using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.UpdateMessage;
using Application.Messages.Queries;
using Application.Messages.Queries.GetAllUsersMessages;
using Application.Messages.Queries.GetChatsMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet("byuserid/{UserId:int?}")]
        public async Task<PaginatedList<MessageDto>> GetAllUsersMessages([FromRoute] GetAllUsersMessagesQuery query)
        {
            var messages = await _sender.Send(query);

            return messages;
        }

        [HttpGet("bychatid/{ChatId:int}")]
        public async Task<PaginatedList<MessageDto>> GetChatsMessages([FromRoute] GetChatsMessagesQuery query)
        {
            var messages = await _sender.Send(query);

            return messages;
        }

        [HttpPost]
        public async Task<int> WriteMessage(CreateMessageCommand command)
        {
            var writtedMessageId = await _sender.Send(command);

            return writtedMessageId;
        }

        [HttpPut]
        public async Task<int> EditMessage(UpdateMessageCommand command)
        {
            var updatedMessageId = await _sender.Send(command);

            return updatedMessageId;
        }

        [HttpDelete("{Id:int}")]
        public async Task<int> DeleteMessage([FromRoute]DeleteMessageCommand command)
        {
            var deletedMessageId = await _sender.Send(command);

            return deletedMessageId;
        }
    }
}
