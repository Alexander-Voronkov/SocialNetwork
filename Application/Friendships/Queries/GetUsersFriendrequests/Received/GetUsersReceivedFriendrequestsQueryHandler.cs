using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Friendships.Queries.GetUsersFriendrequests.Received
{
    public class GetUsersReceivedFriendrequestsQueryHandler : IRequestHandler<GetUsersReceivedFriendrequestsQuery, PaginatedList<FriendshipDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersReceivedFriendrequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FriendshipDto>> Handle(GetUsersReceivedFriendrequestsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var friendRequests = (await _unitOfWork.FriendshipsRepository.FindMany(req =>
                    req.SecondUserId == request.UserId && !req.IsAccepted))
                    .Include(x=>x.FirstUser)
                    .Include(x=>x.SecondUser);

            return await friendRequests
                .Select(x=>_mapper.Map<FriendshipDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
