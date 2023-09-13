using Application.Messages.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
