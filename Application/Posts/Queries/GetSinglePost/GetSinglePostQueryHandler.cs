using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.Queries.GetSinglePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public GetSinglePostQueryHandler(ISender sender, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
            _sender = sender;
        }

        public async Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.GetPostWithReactions((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            var mappedPost = _mapper.Map<PostDto>(post);

            return mappedPost;
        }
    }
}
