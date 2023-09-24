using Application.Messages.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MessageToDto : Profile
    {
        public MessageToDto() 
        {
            CreateMap<Message, MessageDto>();
        }
    }
}
