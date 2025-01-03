using System.Security.Claims;

namespace POSIMSWebApi.Authentication.AuthorizationHelper
{
    public static class AuthorizationHelper
    {
        public static bool HasPermission(ClaimsPrincipal user, string permission)
        {
            // Check if the user has a claim for the given permission
            return user.Claims.Any(c => c.Type == "Permission" && c.Value == permission);
        }
    }
}
