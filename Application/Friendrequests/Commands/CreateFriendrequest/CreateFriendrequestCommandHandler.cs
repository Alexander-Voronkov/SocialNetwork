using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommandHandler : IRequestHandler<CreateFriendrequestCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateFriendrequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateFriendrequestCommand request, CancellationToken cancellationToken)
        {
            var user1 = await _unitOfWork.UsersRepository.Get((int)request.FromId!);
            var user2 = await _unitOfWork.UsersRepository.Get((int)request.ToId!);
            
            if(user1 == null || user2 == null)
            {
                throw new UserNotFoundException();
            }

            var entity = new Friendrequest()
            {
                FromUserId = request.FromId,
                ToUserId = request.ToId
            };

            entity.AddDomainEvent(new CreatedFriendrequestEvent(entity));

            await _unitOfWork.FriendrequestsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
