using Microsoft.IdentityModel.Tokens;
using MyAPI.MyJWT.Contracts;
using MyModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyAPI.MyJWT.Repositories
{
    internal class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"},
         };

        public JWTManagerRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        //public Token? Authenticate(User user)
        //{
        //    if (UserExists(user))
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        //        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        var tokenOptions = new JwtSecurityToken(
        //            issuer: _configuration["JWT:Issuer"],
        //            audience: _configuration["JWT:Audience"],
        //            claims: new List<Claim>(),
        //            expires: DateTime.Now.AddMinutes(5),
        //            signingCredentials: signingCredentials
        //            );

        //        return new Token()
        //        {
        //            Access_Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions)
        //        };
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public Token GenerateRefreshToken(string userName)
        {
            return GetJWT(userName);
        }

        public Token GenerateToken(string userName)
        {
            return GetJWT(userName);
        }

        private Token GetJWT(string userName)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: new Claim[] { new Claim(ClaimTypes.Name, userName) },
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );

                return new Token()
                {
                    Access_Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                    Refresh_Token = GenerateRefreshToken()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        //private bool UserExists(User user)
        //{
        //    return UsersRecords.Any(x => x.Key == user.Name && x.Value == user.Password);
        //}
    }
}
