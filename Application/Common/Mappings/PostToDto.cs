using Application.Posts.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{

    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<Post, PostDto>()
                .ForMember(x => x.Reactions, src => src.MapFrom(x => x.Reactions));
        }
    }
}
