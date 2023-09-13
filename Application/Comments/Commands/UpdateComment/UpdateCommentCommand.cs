using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string? CommentBody { get; set; }
    }
}
