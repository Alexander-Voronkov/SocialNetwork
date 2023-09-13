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

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQueryHandler : IRequestHandler<GetUsersChatsQuery, IEnumerable<ChatDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersChatsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ChatDto>> Handle(GetUsersChatsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }    

            var usersChats = await _unitOfWork.ChatsRepository.Find(chat=>
                chat.FirstUserId == request.UserId ||
                chat.SecondUserId == request.UserId);

            var mappedChats = usersChats
                .ProjectTo<ChatDto>(_mapper.ConfigurationProvider);

            return await mappedChats.ToListAsync();
        }
    }
}
