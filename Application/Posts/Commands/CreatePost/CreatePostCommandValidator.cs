using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.CreatorId)
                .NotNull()
                .WithMessage("Post creator id cannot be null")
                .NotEqual(0)
                .WithMessage("Post creator id cannot be 0");

            RuleFor(x => x.Body)
                .NotNull()
                .WithMessage("Post body cannot be null")
                .NotEmpty()
                .WithMessage("Post body cannot be empty")
                .MaximumLength(800)
                .WithMessage("Post body must be less or equal to 800 symbols");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Post title cannot be empty")
                .NotNull()
                .WithMessage("Post title cannot be null")
                .MaximumLength(50)
                .WithMessage("Post title must be less or equal to 50 symbols");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Post description cannot be empty")
                .NotNull()
                .WithMessage("Post description cannot be null")
                .MaximumLength(200)
                .WithMessage("Post description must be less or equal to 200 symbols");

            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage("There must be at least 1 post tag")
                .NotNull()
                .WithMessage("Post tags cannot be null");

            RuleForEach(x => x.Tags)
                .Must((command, tag) =>
                {
                    var distinctTags = command.Tags!.Where(x => x.Equals(tag));
                    return distinctTags.Count() == 1;
                })
                .WithMessage("Post tags must be unique");
        }
    }
}
