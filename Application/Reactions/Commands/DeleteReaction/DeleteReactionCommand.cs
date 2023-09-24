using MediatR;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommand : IRequest<int>
    {
        public int? ReactionId { get; set; }
    }

}
