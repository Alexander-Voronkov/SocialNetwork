using MediatR;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQuery : IRequest<UserDto>
    {
        public int? UserId { get; set; }
    }
}
