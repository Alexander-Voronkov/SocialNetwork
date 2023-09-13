using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries.GetSingleChat
{
    public class GetSingleChatQueryHandler : IRequestHandler<GetSingleChatQuery, ChatDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetSingleChatQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ChatDto> Handle(GetSingleChatQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ChatsRepository.Get((int)request.ChatId!);

            if(entity == null)
            {
                throw new ChatNotFoundException();
            }

            var mappedEntity = _mapper.Map<ChatDto>(entity);

            return mappedEntity;
        }
    }
}
