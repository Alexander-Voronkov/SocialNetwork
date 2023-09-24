using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQueryHandler : IRequestHandler<GetUsersChatsQuery, PaginatedList<ChatDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersChatsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ChatDto>> Handle(GetUsersChatsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }    

            var usersChats = await _unitOfWork.ChatsRepository.FindMany(chat=>
                chat.FirstUserId == request.UserId ||
                chat.SecondUserId == request.UserId);

            var chatsWithUsers = usersChats
                .Include(x => x.FirstUser)
                .Include(x => x.SecondUser);

            return await chatsWithUsers
                .ProjectTo<ChatDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
