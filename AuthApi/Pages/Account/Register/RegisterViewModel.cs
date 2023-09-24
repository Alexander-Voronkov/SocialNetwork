using System.ComponentModel.DataAnnotations;

namespace AuthApi.Pages.Account.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Enter your email")]
        [Display(Name = "Email: ")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone number: ")]
        [Required(ErrorMessage ="Enter phone number")]
        public string? PhoneNumber { get; set; }
        [Display(Name="Username: ")]
        [Required(ErrorMessage ="Enter username")]
        public string? UserName { get; set; }
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
