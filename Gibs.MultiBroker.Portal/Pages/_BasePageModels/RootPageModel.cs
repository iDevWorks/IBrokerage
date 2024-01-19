using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    public abstract class RootPageModel : PageModel
    {
        public void ShowInfo(string message)
        {
            TempData["ShowInfo"] = message;
        }

        public void ShowError(string message)
        {
            ShowError(message);
        }
    }
}
