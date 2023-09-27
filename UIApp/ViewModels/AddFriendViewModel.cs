using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class AddFriendViewModel
    {
        public PaginatedList<UserDto>? Users { get; set; }
        public string? SearchName { get; set; }
    }
}
