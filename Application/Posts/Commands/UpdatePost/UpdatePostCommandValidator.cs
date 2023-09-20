using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator() 
        {

            RuleFor(x => x.Body)
                .NotNull()
                .WithMessage("Post body cannot be null")
                .NotEmpty()
                .WithMessage("Post body cannot be empty")
                .MaximumLength(100000);

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Post title cannot be empty")
                .NotNull()
                .WithMessage("Post title cannot be null")
                .MaximumLength(200)
                .WithMessage("Post title must be less or equal to 50 symbols");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Post description cannot be empty")
                .NotNull()
                .WithMessage("Post description cannot be null")
                .MaximumLength(1000);

            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage("There must be at least 1 post tag")
                .NotNull()
                .WithMessage("Post tags cannot be null")
                .Must((command, tags) =>
                    tags?.Count() <= 10)
                .WithMessage("Tags count cannot be more than 10");

            RuleForEach(x => x.Tags)
                .Must((command, tag) =>
                {
                    var distinctTags = command.Tags!.Where(x => x.Equals(tag));
                    return distinctTags.Count() == 1;
                })
                .WithMessage("Post tags must be unique");

            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");
        }
    }
}
