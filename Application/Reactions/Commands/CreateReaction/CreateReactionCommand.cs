using Domain.Enums;
using MediatR;

namespace Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommand : IRequest<int>
    {
        public int? PostId { get; set; }
        public ReactionType? Type { get; set; }
    }
}
