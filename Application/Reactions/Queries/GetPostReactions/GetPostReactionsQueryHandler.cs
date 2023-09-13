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

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQueryHandler : IRequestHandler<GetPostReactionsQuery, IEnumerable<ReactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPostReactionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReactionDto>> Handle(GetPostReactionsQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            var reactions = await _unitOfWork.ReactionsRepository.Find(reaction =>
                    reaction.PostId == post.Id);

            var mappedReactions = reactions.ProjectTo<ReactionDto>(_mapper.ConfigurationProvider);

            return await mappedReactions.ToListAsync();
        }
    }
}
