using Application.Friendrequests.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class FriendrequestToDto : Profile
    {
        public FriendrequestToDto() 
        {
            CreateMap<Friendrequest, FriendrequestDto>();
        }
    }
}
