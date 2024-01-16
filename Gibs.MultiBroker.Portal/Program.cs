using Microsoft.EntityFrameworkCore;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Infrastructure.Email;
using Gibs.Portal.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

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

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/public/login/";
                        options.LoginPath = "/public/login";
                        options.Cookie.IsEssential = true;
                    });

            builder.Services.AddDbContext<BrokerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbLocal")));

            //builder.Services.AddTransient<PaystackService>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            var app = builder.Build();
         
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.UseMultiTenant();
            app.Run();
        }
    }
}