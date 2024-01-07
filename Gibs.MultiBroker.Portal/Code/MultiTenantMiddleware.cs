using System.Globalization;

namespace Gibs.Portal.Middleware;

public class MultiTenantMiddleware
{
    private readonly RequestDelegate _next;

    public MultiTenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            var culture = new CultureInfo(cultureQuery);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class MultiTenantMiddlewareExtensions
{
    public static IApplicationBuilder UseMultiTenant(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MultiTenantMiddleware>();
    }
}