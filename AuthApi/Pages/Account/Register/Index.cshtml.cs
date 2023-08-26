using AuthApi.Data;
using AuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace AuthApi.Pages.Account.Register
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel View { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string returnUrl)
        {
            View = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong data entered.");
                return Page();
            }
            var userToBeCreated = new ApplicationUser()
            {
                UserName = View.UserName,
                Email = View.Email,
                PhoneNumber = View.PhoneNumber
            };
            var creationResult = await _userManager.CreateAsync(userToBeCreated, View.Password);
            if (creationResult.Succeeded)
            {
                return Redirect($"~/Account/Login?returnUrl={View.ReturnUrl}");
            }
            else
            {
                ModelState.AddModelError("", creationResult.Errors.Aggregate("", (acc, err) =>
                {
                    return acc + " " + err.Description;
                }));
                return Page();
            }
        }
    }
}
