using System.Security.Claims;

namespace Domain.Security.JWT
{
    public interface ITokenHelper
    {
        //AccessToken CreateTokenForUser(User user, IList<string> roles, string userId, List<string> permissions, List<Claim> customClaims = null!);
    }
}
