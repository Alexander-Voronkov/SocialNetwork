using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotNull()
                .WithMessage("Comment owner id cannot be null");

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
