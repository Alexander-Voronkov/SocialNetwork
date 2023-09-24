using Application.Common.Models;
using MediatR;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQuery : IRequest<PaginatedList<MessageDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
