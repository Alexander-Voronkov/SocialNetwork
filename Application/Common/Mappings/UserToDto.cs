using Application.Users.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class UserToDto : Profile
    {
        public UserToDto() 
        {
            CreateMap<User, UserDto>();
        }
    }
}
