using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSingleUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var mappedUser = _mapper.Map<UserDto>(user);

            return mappedUser;
        }
    }
}
