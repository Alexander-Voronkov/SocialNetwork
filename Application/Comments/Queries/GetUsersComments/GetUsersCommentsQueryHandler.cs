using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Queries.GetUsersComments
{
    public class GetUsersCommentsQueryHandler : IRequestHandler<GetUsersCommentsQuery, PaginatedList<CommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;  
            _mapper = mapper;
        }
        public async Task<PaginatedList<CommentDto>> Handle(GetUsersCommentsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var comments = await _unitOfWork.CommentsRepository.FindMany(comment =>
                    comment.OwnerId == user.Id);

            return await comments
                .Select(x=>_mapper.Map<CommentDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
