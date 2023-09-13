using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            post.Title = request.Title;
            post.Description = request.Description;
            post.Body = request.Body;
            post.Tags = request.Tags;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
