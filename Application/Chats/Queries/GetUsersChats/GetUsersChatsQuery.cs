using Application.Common.Models;
using MediatR;

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQuery : IRequest<PaginatedList<ChatDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
