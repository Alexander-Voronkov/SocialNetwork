using Application.Reactions.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class ReactionToDto : Profile
    {
        public ReactionToDto() 
        {
            CreateMap<Reaction, ReactionDto>();
        }
    }
}
