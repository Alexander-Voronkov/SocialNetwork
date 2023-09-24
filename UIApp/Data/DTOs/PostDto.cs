namespace Data.DTOs
{
    public class PostDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public int? OwnerId { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public UserDto? Owner { get; set; }
        public IEnumerable<ReactionDto>? Reactions { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
    }
}