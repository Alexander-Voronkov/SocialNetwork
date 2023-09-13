using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentsRepository.Get((int)request.Id!);

            if(comment == null)
            {
                throw new CommentNotFoundException();
            }

            await _unitOfWork.CommentsRepository.Remove(comment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
