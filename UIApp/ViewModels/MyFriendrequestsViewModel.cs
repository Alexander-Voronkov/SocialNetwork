using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class MyFriendrequestsViewModel
    {
        public PaginatedList<FriendshipDto>? Friendrequests { get; set; }
    }
}
