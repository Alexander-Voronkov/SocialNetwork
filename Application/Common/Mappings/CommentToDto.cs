using Application.Comments.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class CommentToDto : Profile
    {
        public CommentToDto() 
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}
