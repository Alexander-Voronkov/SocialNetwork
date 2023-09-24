using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UIApp.ViewModels
{
    public class ProfileViewModel
    {
        public int? Id { get; set; }
        [Required]
        [DisplayName("Email: ")]
        public string? Email { get; set; }
        [Required]
        [DisplayName("Phone number: ")]
        public string? PhoneNumber { get; set; }
        [Required]
        [DisplayName("Username: ")]
        public string? Username { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
    }
}
