using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<int>
    {
        public int? Id { get; set; }
    }
}
