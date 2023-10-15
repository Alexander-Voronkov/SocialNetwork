using System.ComponentModel.DataAnnotations;

namespace UIApp.ViewModels
{
    public class EditPostViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(100000)]
        public string? Body { get; set; }
        [Required]
        [MaxLength(10)]
        public string[]? Tags { get; set; }
    }
}
