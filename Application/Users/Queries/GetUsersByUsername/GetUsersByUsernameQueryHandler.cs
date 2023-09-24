using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUsersByUsername
{
    public class GetUsersByUsernameQueryHandler : IRequestHandler<GetUsersByUsernameQuery, PaginatedList<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public GetUsersByUsernameQueryHandler(IUser user, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = user;
        }
        public async Task<PaginatedList<UserDto>> Handle(GetUsersByUsernameQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UsersRepository.FindMany(x=>
                (x.Username!.StartsWith(request.Username!) || 
                x.Username.Contains(request.Username!)) && x.Id != _user.Id);

            var mappedUsers = await users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);

            return mappedUsers;
        }
    }
}
