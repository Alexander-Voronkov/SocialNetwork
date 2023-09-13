using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQueryHandler : IRequestHandler<GetAllUsersMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetAllUsersMessagesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var messages = await _unitOfWork.MessagesRepository.Find(message =>
                    message.OwnerId == request.UserId);

            var mappedMessages = messages.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await mappedMessages.ToListAsync();
        }
    }
}
