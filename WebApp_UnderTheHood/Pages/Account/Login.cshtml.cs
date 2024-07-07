using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp_UnderTheHood.Entities;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var claims = new List<Claim>(); 

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Credential.UserName.Equals("admin") && Credential.Password.Equals("admin"))
            {
                // creating the security context
                 claims = new List<Claim> {
                new Claim(ClaimTypes.Name,Credential.UserName),
                new Claim(ClaimTypes.Email,$"{Credential.UserName}@gmail.com"),
                new Claim("Departmant","HR"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("over18","18")
            };

            }
            else
            {
                 claims = new List<Claim>{
                new Claim(ClaimTypes.Name,Credential.UserName),
                new Claim(ClaimTypes.Email,$"{Credential.UserName}@gmail.com"),
                new Claim("over18","18")
            };
                }

            
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // we add this to make the cookei persistent this means if i logout or close the browser and
            // the cookei not expired i still logined
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = Credential.RememberMe,
               
            };
            // save identity in cookie
            await HttpContext.SignInAsync("MyCookieAuth", principal,authenticationProperties);
            return RedirectToPage("/Index");
        }
    }
}
