using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Friendships.Queries.GetUsersFriendrequests.Sent;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Friendships.Queries.GetAllUsersFriendrequests.Sent
{
    public class GetUsersSentFriendrequestsQueryHandler : IRequestHandler<GetUsersSentFriendrequestsQuery, PaginatedList<FriendshipDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersSentFriendrequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FriendshipDto>> Handle(GetUsersSentFriendrequestsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var friendRequests = (await _unitOfWork.FriendshipsRepository.FindMany(req =>
                    req.FirstUserId == request.UserId && !req.IsAccepted))
                    .Include(x=>x.FirstUser)
                    .Include(x=>x.SecondUser);

            return await friendRequests
                .Select(x=>_mapper.Map<FriendshipDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
