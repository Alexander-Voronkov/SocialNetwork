using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetAllPostComments
{
    public class GetAllPostCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
        public int? PostId { get; set; }
    }
}
