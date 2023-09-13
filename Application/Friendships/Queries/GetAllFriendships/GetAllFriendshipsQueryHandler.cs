using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetAllFriendships
{
    public class GetAllFriendshipsQueryHandler : IRequestHandler<GetAllFriendshipsQuery, IEnumerable<FriendshipDto>>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public GetAllFriendshipsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<FriendshipDto>> Handle(GetAllFriendshipsQuery request, CancellationToken cancellationToken)
        {
            var friendships = await _unitOfWork.FriendshipsRepository.GetAll();

            var mappedFriendships = friendships.ProjectTo<FriendshipDto>(_mapper.ConfigurationProvider);

            return await mappedFriendships.ToListAsync();
        }
    }
}
