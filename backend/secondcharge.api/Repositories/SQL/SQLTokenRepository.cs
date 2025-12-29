using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Repositories.SQL
{
    public class SQLTokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        public SQLTokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Takes a user and their roles, then creates a JWT token string that the client can use for authentication
        public string CreateJWTToken(IdentityUser user, IList<string> roles)
        {
            // Build a list of claims to embed in the token
            var claims = new List<Claim>();

            // Add user's email as a claim
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));

            // Add each role the user has as separate claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create the signing key from the secret in appsettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate the actual JWT token with all the settings
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            // Convert the token object to a string that can be sent to the client
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
