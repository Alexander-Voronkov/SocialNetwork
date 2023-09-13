using Application.Common.Interfaces;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Id = (int)request.Id!,
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            entity.AddDomainEvent(new CreatedUserEvent(entity));

            await _unitOfWork.UsersRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
