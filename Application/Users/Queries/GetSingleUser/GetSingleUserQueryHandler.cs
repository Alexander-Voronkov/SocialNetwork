using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, UserDto>
    {
        public Task<UserDto> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
