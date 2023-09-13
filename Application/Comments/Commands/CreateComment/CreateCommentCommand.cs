using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
    }
}
