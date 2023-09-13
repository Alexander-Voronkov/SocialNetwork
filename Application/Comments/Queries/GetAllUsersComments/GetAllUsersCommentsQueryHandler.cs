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

namespace Application.Comments.Queries.GetAllUsersComments
{
    public class GetAllUsersCommentsQueryHandler : IRequestHandler<GetAllUsersCommentsQuery, IEnumerable<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUsersCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;  
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentDto>> Handle(GetAllUsersCommentsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var comments = await _unitOfWork.CommentsRepository.Find(comment =>
                    comment.OwnerId == user.Id);

            var mappedComments = comments.ProjectTo<CommentDto>(_mapper.ConfigurationProvider);

            return await mappedComments.ToListAsync();
        }
    }
}
