using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class ShowPostsViewModel
    { 
        public PaginatedList<PostDto>? Posts { get; set; }
    }
}
