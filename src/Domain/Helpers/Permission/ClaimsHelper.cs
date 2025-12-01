//using Domain.Constants;
//using Microsoft.AspNetCore.Identity;
//using System.Reflection;
//using System.Security.Claims;

//namespace Domain.Helpers.Permission
//{
//    public static class ClaimsHelper
//    {
//        public static void GetPermissions(this List<RoleClaimsDto> allPermissions, Type policy, string roleId)
//        {
//            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
//            foreach (FieldInfo fi in fields)
//            {
//                string value = fi.GetValue(null).ToString();
//                var valueSplit = value.Split('.');
//                string name = valueSplit[1];
//                string action = valueSplit[2];
//                allPermissions.Add(new RoleClaimsDto { Value = value, Name = name, Action = action, Type = "Permissions" });
//            }
//        }
//        public static async Task AddPermissionClaim(this RoleManager<Role> roleManager, Role role, string permission)
//        {
//            var allClaims = await roleManager.GetClaimsAsync(role);
//            if (!allClaims.Any(a => a.Type == CustomClaimTypes.Permission && a.Value == permission))
//            {
//                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
//            }
//        }
//    }
//}
