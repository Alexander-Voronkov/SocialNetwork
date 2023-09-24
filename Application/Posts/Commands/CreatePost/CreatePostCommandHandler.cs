using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreatePostCommandHandler(IUnitOfWork context, IUser user)
        {
            _unitOfWork = context;
            _user = user;
        }
        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Post
            {
                Body = request.Body,
                Description = request.Description,
                Title = request.Title,
                OwnerId = _user.Id,
                Tags = request.Tags,
            };

            await _unitOfWork.PostsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
