using Microsoft.IdentityModel.Tokens;
using ParkingApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParkingApp.Services
{
    public class JWTService : IAuthService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenGenerationKey")!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) ,
                    new Claim(ClaimTypes.Role, user.Role),
                };

            var token = new JwtSecurityToken(null, null, claims, null, DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return tokenHandler.WriteToken(token);
        }

        public int ValidateToken(string token)
        {
            if (token == null)
            {
                return -1;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenGenerationKey"));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                return userId;
            }
            catch
            {
                return -1;
            }
        }

        public int CheckCookies(IRequestCookieCollection cookies)
        {
            if (cookies == null) return -1;
            if (!cookies.ContainsKey("jwt")) return -1;

            string token = cookies["jwt"];
            int idFromToken = ValidateToken(token);

            if (idFromToken == -1) return -1;

            return idFromToken;
        }

    }
}
