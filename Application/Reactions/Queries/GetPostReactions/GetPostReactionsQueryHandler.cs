using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQueryHandler : IRequestHandler<GetPostReactionsQuery, PaginatedList<ReactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPostReactionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<ReactionDto>> Handle(GetPostReactionsQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            var reactions = await _unitOfWork.ReactionsRepository.FindMany(reaction =>
                    reaction.PostId == post.Id);

            return await reactions
                .ProjectTo<ReactionDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
