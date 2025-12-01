namespace Domain.Security.JWT
{
    public class TokenOptions
    {
        public required string Audience { get; set; }
        public required string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public required string SecurityKey { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
