using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using WebApp.Entities;

namespace WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public RegisterModelView RegisterModelView { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();
            // create user

            var user = new IdentityUser
            {
                Email = RegisterModelView.Email,
                UserName = RegisterModelView.Email,
            };
           var result = await _userManager.CreateAsync(user, RegisterModelView.Password);

            if (result.Succeeded)
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               var confirmationLink = Redirect(Url.PageLink(pageName:"/Account/ConfirmEmail",
                   values: new {userId = user.Id, token = confirmationToken}));

                var message = new MailMessage("hassan19alsayed@gmail.com",
                    RegisterModelView.Email,
                    "please confirm your email",
                    $"please click here to confirm your email address {confirmationLink}");

                using (var emailClient = new SmtpClient("smtp-relay.brevo.com", 587))
                {
                    emailClient.Credentials = new NetworkCredential(
                        "781724001@smtp-brevo.com",
                        "s36TNgBYRmOQCxUW"
                        );
                    await emailClient.SendMailAsync(message);
                }
                return RedirectToPage("/Account/Login");
            }
            result.Errors.ToList().ForEach(e => ModelState.AddModelError("Register", e.Description));
            return Page();
        }
    }
}
