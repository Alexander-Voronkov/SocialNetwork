using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetSingleMessage
{
    public class GetSingleMessageQueryHandler : IRequestHandler<GetSingleMessageQuery, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSingleMessageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDto> Handle(GetSingleMessageQuery request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessagesRepository.Get((int)request.MessageId!);

            if(message == null)
            {
                throw new MessageNotFoundException();
            }

            var mappedMessage = _mapper.Map<MessageDto>(message);

            return mappedMessage;
        }
    }
}
