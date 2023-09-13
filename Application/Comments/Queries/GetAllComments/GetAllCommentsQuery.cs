using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetAllComments
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
    }
}
