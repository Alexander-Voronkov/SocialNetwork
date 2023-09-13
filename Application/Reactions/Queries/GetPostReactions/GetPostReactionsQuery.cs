using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQuery : IRequest<IEnumerable<ReactionDto>>
    {
        public int? PostId { get; set; }
    }
}
