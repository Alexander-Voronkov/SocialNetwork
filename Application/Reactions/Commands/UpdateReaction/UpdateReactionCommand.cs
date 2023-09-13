using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommand : IRequest<int>
    {
        public int ReactionId { get; set; }
        public ReactionType? Type { get; set; }
    }
}
