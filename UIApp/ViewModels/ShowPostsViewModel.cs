using UIApp.Models;

namespace UIApp.ViewModels
{
    public class ShowPostsViewModel
    { 
        public PaginatedList<ShowPostViewModel>? Posts { get; set; }
    }
}
