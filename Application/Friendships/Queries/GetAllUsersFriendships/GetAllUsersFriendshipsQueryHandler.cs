using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetAllUsersFriendships
{
    public class GetAllUsersFriendshipsQueryHandler : IRequestHandler<GetAllUsersFriendshipsQuery, IEnumerable<FriendshipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersFriendshipsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FriendshipDto>> Handle(GetAllUsersFriendshipsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);
        
            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var friendships = await _unitOfWork.FriendshipsRepository.Find(fs =>
                    fs.FirstUserId == user.Id || fs.SecondUserId == user.Id);

            var mappedFriendships = friendships.ProjectTo<FriendshipDto>(_mapper.ConfigurationProvider);

            return mappedFriendships;
        }
    }
}
