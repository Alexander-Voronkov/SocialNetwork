using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project1.Pages.Device
{
    [SecurityHeaders]
    [Authorize]
    public class SuccessModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}