using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class MyChatsViewModel
    {
        public PaginatedList<ChatDto>? Chats { get; set; }
    }
}
