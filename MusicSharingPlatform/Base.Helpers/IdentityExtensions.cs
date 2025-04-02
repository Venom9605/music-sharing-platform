using System.Security.Claims;

namespace Base.Helpers;

public static class IdentityExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        return userId;
    }

}