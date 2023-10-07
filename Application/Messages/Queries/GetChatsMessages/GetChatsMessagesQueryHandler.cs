using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetChatsMessages
{
    public class GetChatsMessagesQueryHandler : IRequestHandler<GetChatsMessagesQuery, PaginatedList<MessageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetChatsMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginatedList<MessageDto>> Handle(GetChatsMessagesQuery request, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.Get((int)request.ChatId!);

            if (chat == null)
            {
                throw new ChatNotFoundException();
            }

            var messages = await _unitOfWork.MessagesRepository.FindMany(message =>
                        message.ChatId == request.ChatId);

            return await messages
                .Select(x=>_mapper.Map<MessageDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
