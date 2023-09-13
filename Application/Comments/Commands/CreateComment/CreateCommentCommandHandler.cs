using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if(request.ReferringCommentId != null)
            {
                var comment = await _unitOfWork.CommentsRepository.Get((int)request.ReferringCommentId!);
                
                if(comment == null)
                {
                    throw new CommentNotFoundException();
                }
            }

            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            var entity = new Comment()
            {
                CommentBody = request.CommentBody,
                ReferringCommentId = request.ReferringCommentId,
                PostId = request.PostId,
                OwnerId = request.OwnerId,
            };

            entity.AddDomainEvent(new CreatedCommentEvent(entity));

            await _unitOfWork.CommentsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
