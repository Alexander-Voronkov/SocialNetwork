using Application.Users.Queries;

namespace Application.Friendrequests.Queries
{
    public class FriendrequestDto
    {
        public int? Id { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public UserDto? From { get; set; }
        public UserDto? To { get; set; }
    }
}
