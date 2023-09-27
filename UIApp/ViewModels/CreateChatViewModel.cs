using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class CreateChatViewModel
    {
        public int? SecondUserId { get; set; }
        public PaginatedList<UserDto>? Users { get; set; }
    }
}
