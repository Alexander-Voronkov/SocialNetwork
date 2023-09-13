using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetAllReactions
{
    public class GetAllUsersReactionsQuery : IRequest<IEnumerable<ReactionDto>>
    {
        public int? UserId { get; set; }
    }
}
