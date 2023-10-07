using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetPostComments
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, PaginatedList<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPostCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CommentDto>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);
            
            if(post == null) 
            {
                throw new PostNotFoundException();
            }

            var comments = (await _unitOfWork.CommentsRepository.FindMany(comment =>
                comment.PostId == request.PostId))
                .Include(x => x.Owner);

            return await comments
                .Select(x=>_mapper.Map<CommentDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
