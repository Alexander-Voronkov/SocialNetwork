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

namespace Application.Comments.Queries.GetAllPostComments
{
    public class GetAllPostCommentsQueryHandler : IRequestHandler<GetAllPostCommentsQuery, PaginatedList<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CommentDto>> Handle(GetAllPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);
            
            if(post == null) 
            {
                throw new PostNotFoundException();
            }
            
            var comments = await _unitOfWork.CommentsRepository.Find(comment=>
                comment.PostId == request.PostId);

            return await comments
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
