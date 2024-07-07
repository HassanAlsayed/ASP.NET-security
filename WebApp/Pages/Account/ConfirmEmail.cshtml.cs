using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; }

        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user is not null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    this.Message = "Email Address Successfully confirmd,you can now try to login";
                    return Page();
                }   
            }
            this.Message = "Failed To Validate Email";
            return Page();
        }
    }
}
