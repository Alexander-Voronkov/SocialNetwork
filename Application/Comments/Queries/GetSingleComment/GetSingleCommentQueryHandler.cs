using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Queries.GetSingleComment
{
    public class GetSingleCommentQueryHandler : IRequestHandler<GetSingleCommentQuery, CommentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSingleCommentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentDto> Handle(GetSingleCommentQuery request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentsRepository.Get((int)request.CommentId!);
            
            if(comment == null)
            {
                throw new CommentNotFoundException();
            }

            var mappedComment = _mapper.Map<CommentDto>(comment);

            return mappedComment;
        }
    }
}
