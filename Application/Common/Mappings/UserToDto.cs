using Application.Users.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
