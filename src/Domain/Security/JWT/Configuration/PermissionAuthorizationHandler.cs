using Domain.Security.Permission;
using Microsoft.AspNetCore.Authorization;

namespace Domain.Security.JWT.Configuration
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
                return;

            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                                x.Value == requirement.Permission);
                                                                //x.Issuer == "http://localhost:7294");
            var x = context.User.Claims.ToList();

            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }

}
