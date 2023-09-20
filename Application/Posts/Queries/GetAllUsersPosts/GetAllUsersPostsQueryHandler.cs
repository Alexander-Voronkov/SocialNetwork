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
using Application.Common.Mappings;
using Application.Users.Queries;
using Application.Common.Models;

namespace Application.Posts.Queries.GetAllUsersPosts
{
    public class GetAllUsersPostsQueryHandler : IRequestHandler<GetAllUsersPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllUsersPostsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<PostDto>> Handle(GetAllUsersPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var posts = await _unitOfWork.PostsRepository.Find(post =>
                    post.OwnerId == request.UserId);

            return await posts
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
