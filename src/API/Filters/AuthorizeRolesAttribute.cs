using Microsoft.AspNetCore.Authorization;

namespace API.Filters
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);

        }
    }
}
