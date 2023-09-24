using Application.Posts.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{

    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<Post, PostDto>();
        }
    }
}
