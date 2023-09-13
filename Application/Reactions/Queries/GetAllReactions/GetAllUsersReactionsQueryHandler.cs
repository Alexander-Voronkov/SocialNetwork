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

namespace Application.Reactions.Queries.GetAllReactions
{
    public class GetAllUsersReactionsQueryHandler : IRequestHandler<GetAllUsersReactionsQuery, IEnumerable<ReactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersReactionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReactionDto>> Handle(GetAllUsersReactionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var reactions = await _unitOfWork.ReactionsRepository.Find(reaction =>
                        reaction.OwnerId == user.Id);

            var mappedReactions = reactions.ProjectTo<ReactionDto>(_mapper.ConfigurationProvider);

            return await mappedReactions.ToListAsync();
        }
    }
}
