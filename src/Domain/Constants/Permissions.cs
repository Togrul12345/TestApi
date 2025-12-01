namespace Domain.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModuel(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}",
                $"Permissions.{module}",
                $"Permissions.{module}",
                $"Permissions.{module}",
            };
        }
        public static class PermissionLists
        {
            public const string UsersCreate = "Permissions.Users.Create";
            public const string UsersDelete = "Permissions.Users.Delete";
            public const string UsersUpdate = "Permissions.Users.Update";
            public const string UsersGet = "Permissions.Users.Get";

            public const string RolesCreate = "Permissions.Roles.Create";
            public const string RolesDelete = "Permissions.Roles.Delete";
            public const string RolesUpdate = "Permissions.Roles.Update";
            public const string RolesGet = "Permissions.Roles.Get";
            public const string RolesAssignRole = "Permissions.Roles.AssignRole";
            
        }
    }
}
