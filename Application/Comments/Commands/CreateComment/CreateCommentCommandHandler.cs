using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
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
                OwnerId = _user.Id
            };

            entity.AddDomainEvent(new CreatedCommentEvent(entity));

            await _unitOfWork.CommentsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
