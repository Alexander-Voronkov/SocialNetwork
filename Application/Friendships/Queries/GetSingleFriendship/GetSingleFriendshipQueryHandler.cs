using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetSingleFriendship
{
    public class GetSingleFriendshipQueryHandler : IRequestHandler<GetSingleFriendshipQuery, FriendshipDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSingleFriendshipQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FriendshipDto> Handle(GetSingleFriendshipQuery request, CancellationToken cancellationToken)
        {
            var friendship = await _unitOfWork.FriendshipsRepository.Get((int)request.FriendshipId!);

            if(friendship == null)
            {
                throw new FriendshipNotFoundException();
            }

            var mappedFriendship = _mapper.Map<FriendshipDto>(friendship);

            return mappedFriendship;
        }
    }
}
