using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQuery : IRequest<PaginatedList<ReactionDto>>
    {
        public int? PostId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
