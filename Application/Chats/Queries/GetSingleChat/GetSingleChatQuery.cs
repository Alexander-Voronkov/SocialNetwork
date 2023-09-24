using MediatR;

namespace Application.Chats.Queries.GetSingleChat
{
    public class GetSingleChatQuery : IRequest<ChatDto>
    {
        public int? ChatId { get; set; }
    }
}
