using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class MyFriendsViewModel
    {
        public PaginatedList<FriendshipDto>? Friends { get; set; }
    }
}
