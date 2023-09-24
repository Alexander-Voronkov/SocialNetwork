using Application.Chats.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class ChatToDto : Profile
    {
        public ChatToDto() 
        {
            CreateMap<Chat, ChatDto>();
        }
    }
}
