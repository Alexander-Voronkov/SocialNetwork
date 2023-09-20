using Data.DTOs;

namespace UIApp.ViewModels
{
    public class ProfileViewModel
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
    }
}
