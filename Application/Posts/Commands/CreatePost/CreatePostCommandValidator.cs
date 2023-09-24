using FluentValidation;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {

            RuleFor(x => x.Body)
                .NotNull()
                .WithMessage("Post body cannot be null")
                .NotEmpty()
                .WithMessage("Post body cannot be empty")
                .MaximumLength(100000)
                .WithMessage("Post body must be less or equal to 800 symbols");

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
                .MaximumLength(1000)
                .WithMessage("Post description must be less or equal to 200 symbols");

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
                    if (string.IsNullOrWhiteSpace(tag))
                        return false;
                    var distinctTags = command.Tags!.Where(x => x.Equals(tag));
                    return distinctTags.Count() == 1;
                })
                .WithMessage("Post tags must be unique");
        }
    }
}
