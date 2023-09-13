using Application.Chats.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
