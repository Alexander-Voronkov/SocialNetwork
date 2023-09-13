using Application.Reactions.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
