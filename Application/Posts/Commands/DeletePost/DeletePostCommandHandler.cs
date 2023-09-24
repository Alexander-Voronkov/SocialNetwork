using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            await _unitOfWork.PostsRepository.Remove(post);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
