using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetAllReactions
{
    public class GetAllReactionsQueryHandler : IRequestHandler<GetAllReactionsQuery, List<ReactionDto>>
    {
        public Task<List<ReactionDto>> Handle(GetAllReactionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
