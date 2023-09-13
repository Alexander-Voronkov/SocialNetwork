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

namespace Application.Friendrequests.Queries.GetAllFriendrequests
{
    public class GetAllFriendrequestsQueryHandler : IRequestHandler<GetAllFriendrequestsQuery, IEnumerable<FriendrequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllFriendrequestsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FriendrequestDto>> Handle(GetAllFriendrequestsQuery request, CancellationToken cancellationToken)
        {
            var friendRequests = await _unitOfWork.FriendrequestsRepository.GetAll();
            
            var mappedFriendRequests = friendRequests.ProjectTo<FriendrequestDto>(_mapper.ConfigurationProvider);

            return await mappedFriendRequests.ToListAsync();
        }
    }
}
