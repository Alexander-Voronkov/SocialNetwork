using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Application.Common.Mappings;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetUsersPosts
{
    public class GetUsersPostsQueryHandler : IRequestHandler<GetUsersPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersPostsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<PostDto>> Handle(GetUsersPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var posts = (await _unitOfWork.PostsRepository.FindMany(post =>
                    post.OwnerId == request.UserId))
                    .Include(x=>x.Reactions);

            return await posts
                .Select(x=>_mapper.Map<PostDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
