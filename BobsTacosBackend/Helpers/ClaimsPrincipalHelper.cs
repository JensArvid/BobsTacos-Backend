using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BobsTacosBackend.Helpers
{
    public static class ClaimsPrincipalHelper
    {
        public static string? UserId(this ClaimsPrincipal user)
        {
            Claim? claim = user.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
        public static string? Email(this ClaimsPrincipal user)
        {
            Claim? claim = user.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
