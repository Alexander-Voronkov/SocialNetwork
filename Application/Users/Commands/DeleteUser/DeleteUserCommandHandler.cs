using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.Id!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            await _unitOfWork.UsersRepository.Remove(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
