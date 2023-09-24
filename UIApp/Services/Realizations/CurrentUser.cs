using UIApp.Services.Interfaces;

namespace UIApp.Services.Realizations
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? Id 
        { 
            get
            {
                try
                {
                    var userId = _httpContextAccessor.HttpContext!.User.Identities.ElementAt(0).Claims.ElementAt(2).Value;
                    return int.Parse(userId!);
                }
                catch {
                    return 0;
                }
            }
        }
    }
}
