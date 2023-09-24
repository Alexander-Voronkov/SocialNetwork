using Application.Common.Models;
using MediatR;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQuery : IRequest<PaginatedList<ReactionDto>>
    {
        public int? PostId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
