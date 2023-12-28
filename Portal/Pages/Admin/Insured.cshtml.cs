using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gibs.Portal.Pages
{
    public class InsuredModel(ILogger<InsuredModel> logger, IBrokerageContext context, IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly string _currUserId = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public List<Client> Clients { get; set; } = [];

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Address { get; set; } = string.Empty;

        public async Task<PageResult> OnGetAsync()
        {
            Clients = await context.Clients.Where(c => c.BrokerId == _currUserId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var broker = await context.Brokers.SingleOrDefaultAsync(b => b.Id == _currUserId);

                    if (broker == null)
                        throw new Exception("Invalid Broker ID");

                    var clientExists = await context.Clients.AnyAsync(c => c.BrokerId == _currUserId && c.Email == Email);

                    if (clientExists)
                        throw new Exception("A client with this email already exists.");

                    var fullName = $"{FirstName} {LastName}";
                    var client = new Client(broker, fullName, Address, Email, PhoneNumber);

                    context.Clients.Add(client);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in : {Message}", ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage(); //why not Page()?
        }
    }
}
