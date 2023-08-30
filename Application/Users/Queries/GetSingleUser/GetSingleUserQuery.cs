using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQuery : IRequest<UserDto>
    {
        public int? UserId { get; set; }
    }
}
