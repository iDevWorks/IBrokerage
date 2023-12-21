using Microsoft.EntityFrameworkCore;
using Portal.EntityFramework;
using Portal.Services;

namespace iBrokerage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //                .AddCookie(options =>
            //                {
            //                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //                    options.SlidingExpiration = true;
            //                    options.AccessDeniedPath = "/Forbidden/";
            //                    options.LoginPath = "/Index";
            //                    options.Cookie.IsEssential = true;
            //                });

            // Use in-memory database for demo purposes;
            builder.Services.AddDbContextPool<IBrokerageContext>(options =>
                options.UseInMemoryDatabase("IBrokerage"));

            builder.Services.AddTransient<EmailSender>();
            //builder.Services.AddScoped<IPasswordHasher<Broker>, PasswordHasher<Broker>>();
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            var app = builder.Build();
         
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}