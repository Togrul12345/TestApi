using Microsoft.AspNetCore.Authorization;

namespace API.Filters
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
