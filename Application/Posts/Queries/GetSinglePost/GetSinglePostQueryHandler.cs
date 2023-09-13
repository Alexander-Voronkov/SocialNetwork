using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetSignlePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSinglePostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }
        public async Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            var mappedPost = _mapper.Map<PostDto>(post);

            return mappedPost;
        }
    }
}
