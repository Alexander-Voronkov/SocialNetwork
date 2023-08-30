using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommand : IRequest<int>
    {
        public int? PostId { get; set; }
        public ReactionType? Type { get; set; }
        public int? OwnerId { get; set; }
    }
}
