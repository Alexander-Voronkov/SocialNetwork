using Application.Chats.Commands.CreateChat;
using Application.Chats.Commands.DeleteChat;
using Application.Chats.Queries;
using Application.Chats.Queries.GetSingleChat;
using Application.Chats.Queries.GetUsersChats;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
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

        public ChatsController(ISender sender, IUser user)
        {
            _sender = sender;
            _user = user;
        }

        [HttpGet("byuserid/{UserId:int}")]
        public async Task<PaginatedList<ChatDto>> GetAllUsersChats([FromRoute]GetUsersChatsQuery query)
        {
            var chats = await _sender.Send(query);

            return chats;
        }

        [HttpGet("byid/{ChatId:int}")]
        public async Task<ChatDto> GetSingleChat([FromRoute] GetSingleChatQuery query)
        {
            var chat = await _sender.Send(query);

            return chat;
        }

        [HttpPost]
        public async Task<int> Post(CreateChatCommand command)
        {
            var createdChatId = await _sender.Send(command);
        
            return createdChatId;
        }

        [HttpDelete("{ChatId:int}")]
        public async Task<int> Delete([FromRoute] DeleteChatCommand command)
        {
            var deletedChatId = await _sender.Send(command);

            return deletedChatId;
        }
    }
}
