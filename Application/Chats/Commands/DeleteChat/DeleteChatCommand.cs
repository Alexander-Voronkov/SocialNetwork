using MediatR;

namespace Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommand : IRequest<int>
    {
        public int? ChatId { get; set; }
    }
}
