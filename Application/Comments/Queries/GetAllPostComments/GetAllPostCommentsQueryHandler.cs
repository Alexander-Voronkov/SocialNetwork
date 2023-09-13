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

namespace Application.Comments.Queries.GetAllPostComments
{
    public class GetAllPostCommentsQueryHandler : IRequestHandler<GetAllPostCommentsQuery, IEnumerable<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetAllPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);
            
            if(post == null) 
            {
                throw new PostNotFoundException();
            }
            
            var comments = await _unitOfWork.CommentsRepository.Find(comment=>
                comment.PostId == request.PostId);

            var mappedComments = comments.ProjectTo<CommentDto>(_mapper.ConfigurationProvider);

            return await mappedComments.ToListAsync();
        }
    }
}
