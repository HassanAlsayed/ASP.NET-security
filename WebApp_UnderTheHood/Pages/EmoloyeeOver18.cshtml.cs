using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "HumanRessorcesOnly")]
    public class EmoloyeeOver18Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
