using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetAllUsersComments
{
    public class GetAllUsersCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
        public int? UserId { get; set; }
    }
}
