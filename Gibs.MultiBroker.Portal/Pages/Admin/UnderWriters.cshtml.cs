using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages.Admin
{
    public class UnderWritersModel(BrokerContext context) : PageModel
    {
        private readonly BrokerContext _context = context;

        public List<Underwriter> UnderWriters { get; set; } = [];

        public async Task<PageResult> OnGetAsync()
        {
            UnderWriters = await _context.Underwriters.ToListAsync();

            return Page();
        }

        public async Task<ActionResult> OnPostDeleteUnderWriter(string underwriterId)
        {
            var underWriter = UnderWriters.SingleOrDefault(u => u.Id == underwriterId);
            if (underWriter != null)
            {
                _context.Underwriters.Remove(underWriter);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
