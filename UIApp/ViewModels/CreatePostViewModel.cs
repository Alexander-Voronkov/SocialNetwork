using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UIApp.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        [MaxLength(200)]
        [DisplayName("Title")]
        public string? Title { get; set; }
        [Required]
        [MaxLength(100000)]
        [DisplayName("Body")]
        public string? Body { get; set; }
        [Required]
        [MaxLength(1000)]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required]
        [MaxLength(10)]
        public string[]? Tags { get; set; }
    }
}
