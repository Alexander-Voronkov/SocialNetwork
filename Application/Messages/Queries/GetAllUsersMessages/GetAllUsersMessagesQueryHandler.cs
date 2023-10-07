using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQueryHandler : IRequestHandler<GetAllUsersMessagesQuery, PaginatedList<MessageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<MessageDto>> Handle(GetAllUsersMessagesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var messages = await _unitOfWork.MessagesRepository.FindMany(message =>
                    message.OwnerId == request.UserId);

            return await messages
                .Select(x=>_mapper.Map<MessageDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
