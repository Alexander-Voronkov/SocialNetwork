using Application.Friendships.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class FriendshipToDto : Profile
    {
        public FriendshipToDto() 
        {
            CreateMap<Friendship, FriendshipDto>();
        }
    }
}
