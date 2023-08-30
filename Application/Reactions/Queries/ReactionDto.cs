using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Reactions.Queries
{
    public class ReactionDto
    {
        public ReactionType? Type { get; set; }
        public int? OwnerId { get; set; }
        public int? PostId { get; set; }

        private class Mapping : Profile
        {
            public Mapping() 
            {
                CreateMap<Reaction, ReactionDto>();
            }
        }
    }
}
