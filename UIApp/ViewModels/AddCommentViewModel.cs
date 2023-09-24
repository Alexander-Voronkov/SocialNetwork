using System.ComponentModel.DataAnnotations;

namespace UIApp.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public int? PostId { get; set; }
        [Required]
        public string? CommentBody { get; set; }
        public int? ReferringCommentId { get; set; }
    }
}
