using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Entities;

namespace WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [BindProperty]
        public Credential Credential { get; set; }

        [BindProperty]
        public IEnumerable<AuthenticationScheme> AuthenticationProviders { get; set; }
        public async Task OnGetAsync()
        {
            this.AuthenticationProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(Credential.Email,
                Credential.Password,
                Credential.RememberMe,
                false);

            if(result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError("Login", "Failed to login");
            return Page();
        }

        public IActionResult OnPostLoginExternally(string provider)
        {
            // properties contains some info about user
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider,null);
            properties.RedirectUri = Url.Action("ExternalLoginCallback", "Account");

            return Challenge(properties,provider);
        }
    }
}
