using MediatR;

namespace Application.Chats.Commands.CreateChat
{
    public class CreateChatCommand : IRequest<int>
    {
        public int? SecondUserId { get; set; }
    }
}
