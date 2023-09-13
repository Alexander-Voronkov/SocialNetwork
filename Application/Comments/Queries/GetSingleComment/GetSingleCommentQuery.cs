using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetSingleComment
{
    public class GetSingleCommentQuery : IRequest<CommentDto>
    {
        public int? CommentId { get; set; }
    }
}
