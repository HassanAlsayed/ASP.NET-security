using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "HumanRessorcesOnly")]
    public class HumanResorcesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
