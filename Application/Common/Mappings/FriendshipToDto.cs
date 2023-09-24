using Application.Friendships.Queries;
using AutoMapper;
using Domain.Entities;

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
