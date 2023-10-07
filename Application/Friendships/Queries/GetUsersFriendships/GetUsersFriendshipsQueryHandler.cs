using Application.Common.Exceptions;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Queries.GetUsersFriendships
{
    public class GetUsersFriendshipsQueryHandler : IRequestHandler<GetUsersFriendshipsQuery, PaginatedList<FriendshipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersFriendshipsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<FriendshipDto>> Handle(GetUsersFriendshipsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.UserId!);
        
            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var friendships = await _unitOfWork.FriendshipsRepository.FindMany(fs =>
                    (fs.FirstUserId == user.Id || fs.SecondUserId == user.Id) && fs.IsAccepted);

            return await friendships
                .Select(x=>_mapper.Map<FriendshipDto>(x))
                .PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}
