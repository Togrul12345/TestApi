namespace Domain.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public required string RefreshToken { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
