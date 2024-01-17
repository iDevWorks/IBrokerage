using Microsoft.EntityFrameworkCore;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Infrastructure.Email;
using Gibs.Portal.Middleware;
using iDevWorks.Paystack;

namespace Gibs.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settings = builder.Configuration.GetSection("SmtpSettings").Get<Settings>() 
                ?? throw new InvalidOperationException("SmtpSettings configuration is missing!!!");

            builder.Services.AddAuthentication("Cookies").AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromHours(10);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/Public/Login/";
                        options.LoginPath = "/Public/Login";
                        options.Cookie.IsEssential = true;
                    });

            builder.Services.AddDbContext<BrokerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb")));

            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            var paystack = new PaystackClient("sk_test_56ad4dcb51028c01db37201b5d74f69032c4b9eb");

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton(paystack);
            builder.Services.AddSingleton<EmailService>();
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpSettings"));

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