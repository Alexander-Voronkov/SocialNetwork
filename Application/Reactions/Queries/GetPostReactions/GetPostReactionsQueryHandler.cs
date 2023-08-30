using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQueryHandler : IRequestHandler<GetPostReactionsQuery, int>
    {
        public Task<int> Handle(GetPostReactionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
