using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Infrastructure;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Infrastructure.Email;
using Gibs.Portal.Middleware;
using iDevWorks.Paystack;
using Gibs.Portal;


var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("SmtpSettings").Get<Settings>()
    ?? throw new InvalidOperationException("SmtpSettings configuration is missing!!!");

builder.Services.AddDbContextPool<BrokerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb"));
});

builder.Services.AddAuthentication("Cookies").AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
    options.AccessDeniedPath = "/Public/Login/";
    options.LoginPath = "/Public/Login";
    options.Cookie.IsEssential = true;
    options.SlidingExpiration = true;
});

builder.Services.AddRazorPages(options =>
{
    //options.RootDirectory = "/Pages";
    //options.Conventions.AuthorizeFolder("/Pages/Admin");
    //options.Conventions.AuthorizeFolder("/Pages/Broker");
    //options.Conventions.AuthorizeFolder("/Pages/Customer");
})
.AddMvcOptions(options =>
 {
     options.Filters.Add<SerializeModelStatePageFilter>();
 });

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
