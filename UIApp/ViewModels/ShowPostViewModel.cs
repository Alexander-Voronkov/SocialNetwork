using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class ShowPostViewModel
    {
        public PostDto? Post { get; set; }
        public PaginatedList<CommentDto>? Comments { get; set; }
        public int? LikesCount { get; set; }
        public int? DislikesCount { get; set; }
        public int? AngersCount { get; set; }
        public int? CryingsCount { get; set; }
        public int? LaughsCount { get; set; }
    }
}
