    namespace Domain.Helpers.Permission
{
    public class RoleClaimsDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
