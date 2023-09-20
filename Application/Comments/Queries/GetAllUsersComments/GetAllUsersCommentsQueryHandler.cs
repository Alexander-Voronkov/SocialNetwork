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

namespace Application.Comments.Queries.GetAllUsersComments
{
    public class GetAllUsersCommentsQueryHandler : IRequestHandler<GetAllUsersCommentsQuery, PaginatedList<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;  
            _mapper = mapper;
        }
        public async Task<PaginatedList<CommentDto>> Handle(GetAllUsersCommentsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var comments = await _unitOfWork.CommentsRepository.Find(comment =>
                    comment.OwnerId == user.Id);

            return await comments
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
