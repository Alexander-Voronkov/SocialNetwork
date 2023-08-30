using Application.Reactions.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Posts.Queries
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public IEnumerable<ReactionDto>? Reactions { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Post, PostDto>()
                    .ForMember(x=>x.Reactions, src => src.MapFrom(x=>x.Reactions));
            }
        }
    }
}