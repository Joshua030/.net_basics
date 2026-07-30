using ABCSharedLibrary.Constants;
using Microsoft.AspNetCore.Authorization;

namespace App.Infrastructure.Services.Auth
{
    public class ShouldHavePermission : AuthorizeAttribute
    {
        public ShouldHavePermission(string action, string feature)
        {
            Policy = SchoolPermission.NameFor(action, feature);
        }

    }
}
