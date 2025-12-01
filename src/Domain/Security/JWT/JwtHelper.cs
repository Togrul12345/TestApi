using Domain.Entities.UserEntity;
using Domain.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Domain.Security.JWT;
public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
    }

    #region CreateTokenFor User and Company

    public AccessToken CreateTokenForUser(AppUser user, IList<string> roles, string userId, List<string> permissions, List<Claim> customClaims = null)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles, userId, permissions, customClaims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenLifeTime = _tokenOptions.RefreshTokenLifeTime
        };
    }

    #endregion

    #region CreateJwtSecurityToken
    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AppUser user, //set specific to user
        SigningCredentials signingCredentials, IList<string>? roles, string userId, List<string>? permissions, List<Claim> customClaims)
    {
        var claims = SetClaims(user, roles, userId, permissions, customClaims);
        var jwt = new JwtSecurityToken(
             issuer: tokenOptions.Issuer,
             audience: tokenOptions.Audience,
             expires: _accessTokenExpiration,
             notBefore: DateTime.Now,
             claims: claims,
             signingCredentials: signingCredentials
         );
        return jwt;
    }

    #endregion

    #region SetClaims
    private IEnumerable<Claim> SetClaims(AppUser user, IList<string> roles, string userId, List<string> permissions, List<Claim> customClaims)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        roles.ToList().ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
        permissions.ToList().ForEach(permission => claims.Add(new("Permission", permission)));
        if (customClaims != null) claims.AddRange(customClaims);
        return claims;
    }
    //private IEnumerable<Claim> SetClaimsWithoutJWT(Company company, string companyId, List<string> permissions, List<Claim> customClaims)
    //{
    //    var claims = new List<Claim>
    //    {
    //        new(ClaimTypes.Name,company.Name),
    //        new(ClaimTypes.NameIdentifier,companyId)
    //    };
    //    permissions.ToList().ForEach(permission => claims.Add(new("Permission", permission)));
    //    if (customClaims != null) claims.AddRange(customClaims);
    //    return claims;
    //}
    #endregion

    private string CreateRefreshToken()

    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);

    }
}
