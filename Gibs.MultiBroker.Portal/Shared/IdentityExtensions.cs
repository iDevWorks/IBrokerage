using Microsoft.AspNetCore.Authentication.Cookies;
using Gibs.Domain.Entities;

namespace System.Security.Claims
{
    public static class IdentityExtensions
    {
        //public enum AuthScheme { COOKIES, JWT }

        public static ClaimsPrincipal AsPrincipal(this Person person, 
            string role)//, AuthScheme authScheme = AuthScheme.COOKIE)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, person.Id.ToString()),
                new(ClaimTypes.Email, person.Email),
                new(ClaimTypes.Name, person.FullName),
                new(ClaimTypes.Surname, person.LastName),
                new(ClaimTypes.GivenName, person.FirstName),
                new(ClaimTypes.Uri, "https://th.bing.com/th/id/R.29398e5a94ac8c62bdfcc4f960ec9037?rik=yNwpJ7i2YUsNhA&pid=ImgRaw&r=0"),
                new(ClaimTypes.Role, role),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public static string? GetClaimValue(this ClaimsPrincipal principal, string claimType)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == claimType);
            //if (claim == null) return null;
            return claim?.Value;
        }
    }
}
