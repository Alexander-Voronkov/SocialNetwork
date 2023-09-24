using MediatR;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
