using ABCSharedLibrary.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace App.Infrastructure.Extensions
{
    public static class AuthorizationServiceExtension
    {

        public static async Task<bool> HasPermissionAsync(this IAuthorizationService authorizationService, ClaimsPrincipal user, string feature, string action)
        {
            return (await authorizationService.AuthorizeAsync(user, null, SchoolPermission.NameFor(action, feature))).Succeeded;
        }
    }
}
