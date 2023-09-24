namespace Data.DTOs
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
        public UserDto? Owner { get; set; }
        public CommentDto? ReferringComment { get; set; }
        public PostDto? Post { get; set; }
    }
}
