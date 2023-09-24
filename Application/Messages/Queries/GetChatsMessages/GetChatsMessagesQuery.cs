using Application.Common.Models;
using MediatR;

namespace Application.Messages.Queries.GetChatsMessages
{
    public class GetChatsMessagesQuery : IRequest<PaginatedList<MessageDto>>
    {
        public int? ChatId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
