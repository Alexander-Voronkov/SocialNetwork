namespace Data.DTOs
{
    public class ChatDto
    {
        public int? Id { get; set; }
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
        public UserDto? FirstUser { get; set; }
        public UserDto? SecondUser { get; set; }
        public IEnumerable<MessageDto>? Messages { get;set; }
    }
}
