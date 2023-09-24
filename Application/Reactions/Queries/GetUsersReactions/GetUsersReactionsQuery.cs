using Application.Common.Models;
using MediatR;

namespace Application.Reactions.Queries.GetUsersReactions
{
    public class GetUsersReactionsQuery : IRequest<PaginatedList<ReactionDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
