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

namespace Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received
{
    public class GetAllUsersSentFriendrequestsQueryHandler : IRequestHandler<GetAllUsersSentFriendrequestsQuery, IEnumerable<FriendrequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllUsersSentFriendrequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FriendrequestDto>> Handle(GetAllUsersSentFriendrequestsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var friendRequests = await _unitOfWork.FriendrequestsRepository.Find(req =>
                    req.FromUserId == request.UserId);

            var mappedFriendRequests = friendRequests.ProjectTo<FriendrequestDto>(_mapper.ConfigurationProvider);

            return await mappedFriendRequests.ToListAsync();
        }
    }
}
