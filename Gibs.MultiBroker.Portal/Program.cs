using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Infrastructure.Email;
using Gibs.Portal.Services;
using Gibs.Portal.Middleware;

namespace Gibs.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();

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
            builder.Services.AddDbContextPool<BrokerContext>(options =>
                options.UseInMemoryDatabase("IBrokerage"));

            //builder.Services.AddTransient<PaystackService>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            var app = builder.Build();
         
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapRazorPages();
            app.UseMultiTenant();
            app.Run();
        }
    }
}