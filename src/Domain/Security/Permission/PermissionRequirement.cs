using Microsoft.AspNetCore.Authorization;

namespace Domain.Security.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }
        public PermissionRequirement()
        {

        }
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
