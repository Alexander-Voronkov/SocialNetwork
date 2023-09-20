using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
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
    public class GetAllUsersReactionsQueryHandler : IRequestHandler<GetAllUsersReactionsQuery, PaginatedList<ReactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersReactionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ReactionDto>> Handle(GetAllUsersReactionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var reactions = await _unitOfWork.ReactionsRepository.Find(reaction =>
                        reaction.OwnerId == user.Id);

            return await reactions
                .ProjectTo<ReactionDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
