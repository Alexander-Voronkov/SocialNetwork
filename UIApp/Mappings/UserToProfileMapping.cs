using AutoMapper;
using Data.DTOs;
using UIApp.ViewModels;

namespace UIApp.Mappings
{
    public class UserToProfileMapping : Profile
    {
        public UserToProfileMapping() 
        {
            CreateMap<UserDto, ProfileViewModel>()
                .ReverseMap();
        }
    }
}
