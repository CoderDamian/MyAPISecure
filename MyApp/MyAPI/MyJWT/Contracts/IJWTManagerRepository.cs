using MyModels;
using System.Security.Claims;

namespace MyAPI.MyJWT.Contracts
{
    public interface IJWTManagerRepository
    {
        //public Token? Authenticate(User user);

        Token GenerateToken(string userName);
        Token GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
