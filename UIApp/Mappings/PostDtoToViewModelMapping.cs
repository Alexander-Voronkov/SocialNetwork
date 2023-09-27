using AutoMapper;
using Data.DTOs;
using UIApp.ViewModels;

namespace UIApp.Mappings
{
    public class PostDtoToViewModelMapping : Profile
    {
        public PostDtoToViewModelMapping() 
        {
            CreateMap<PostDto, EditPostViewModel>();
        }
    }
}
