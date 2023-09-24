using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Reactions.Queries.GetUsersReactions
{
    public class GetUsersReactionsQueryHandler : IRequestHandler<GetUsersReactionsQuery, PaginatedList<ReactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersReactionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ReactionDto>> Handle(GetUsersReactionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var reactions = await _unitOfWork.ReactionsRepository.FindMany(reaction =>
                        reaction.OwnerId == user.Id);

            return await reactions
                .ProjectTo<ReactionDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
