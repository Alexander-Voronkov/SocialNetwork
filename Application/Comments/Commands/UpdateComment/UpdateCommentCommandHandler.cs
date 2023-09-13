using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentsRepository.Get((int)request.Id!);
        
            if(comment == null)
            {
                throw new CommentNotFoundException();
            }

            comment.CommentBody = request.CommentBody;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
