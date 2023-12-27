using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portal.Entities;
using Portal.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace iBrokerage.Pages
{
    public class InsuredModel : PageModel
    {
        private readonly IBrokerageContext _context;
        private readonly string _currUserId;
        private ILogger<InsuredModel> _logger;

        public List<Client> Clients { get; set; } = [];

        public InsuredModel(ILogger<InsuredModel> logger, IBrokerageContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _currUserId = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [BindProperty, StringLength(20, MinimumLength = 4)]
        public string FirstName { get; set; }

        [BindProperty, StringLength(20, MinimumLength = 4)]
        public string LastName { get; set; }

        [BindProperty, EmailAddress]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [BindProperty, StringLength(100, MinimumLength = 10)]
        public string Address { get; set; }

        public async Task<PageResult> OnGetAsync()
        {
            var client = new Client();
            Clients = await _context.Clients.Where(c => c.BrokerId == _currUserId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            try
            {
                var broker = await _context.Brokers.SingleOrDefaultAsync(b => b.Id == _currUserId);
                
                var ClientExists = await _context.Clients.AnyAsync(c => c.Email == Email);

                if (ClientExists)
                {
                    throw new Exception("A client with this email already exists.");
                }

                var fullName = $"{FirstName} {LastName}";
                var client = new Client(broker, fullName, Address, Email, PhoneNumber);

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding the client");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage();
        }
    }
}
