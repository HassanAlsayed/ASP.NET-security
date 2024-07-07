using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Entities;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        // return user info
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var LoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (LoginInfo is not null)
            {
                var emailClaim = LoginInfo.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                var NameClaim = LoginInfo.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

                if (emailClaim is not null && NameClaim is not null)
                {
                    var user = new IdentityUser { Email = emailClaim.Value, UserName = NameClaim.Value };
                    await _signInManager.SignInAsync(user, false);
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
