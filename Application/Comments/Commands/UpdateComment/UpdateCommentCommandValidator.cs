using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Comment id cannot be null");

            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");

            RuleFor(x => x.CommentBody)
                .NotEmpty()
                .WithMessage("Comment body cannot be empty")
                .NotNull()
                .WithMessage("Comment body cannot be null");
        }
    }
}
