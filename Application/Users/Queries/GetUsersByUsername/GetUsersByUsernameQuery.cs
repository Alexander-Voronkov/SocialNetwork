using Application.Common.Models;
using MediatR;

namespace Application.Users.Queries.GetUsersByUsername
{
    public class GetUsersByUsernameQuery : IRequest<PaginatedList<UserDto>>
    {
        public string? Username { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
