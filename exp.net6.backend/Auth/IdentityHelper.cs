using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace exp.net6.backend.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserID(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static List<string> GetRoles(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }

        public static string? GetRole(this ClaimsPrincipal principal)
        {
            return principal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

        }
    }
}
