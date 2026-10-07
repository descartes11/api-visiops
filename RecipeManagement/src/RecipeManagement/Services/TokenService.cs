namespace RecipeManagement.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RecipeManagement.Domain.Users;
using RecipeManagement.Domain.Users.Dtos;
using Resources;

public interface ITokenService : IRecipeManagementScopedService
{
    TokenDto CreateToken(ApplicationUser user);
}

public sealed class TokenService(IConfiguration configuration, TimeProvider timeProvider) : ITokenService
{
    public TokenDto CreateToken(ApplicationUser user)
    {
        var jwt = configuration.GetJwtOptions();
        if (jwt is null || Encoding.UTF8.GetByteCount(jwt.Key) < RecipeManagementOptions.JwtOptions.MinKeyLength)
        {
            throw new InvalidOperationException(
                $"The JWT signing key is missing or shorter than {RecipeManagementOptions.JwtOptions.MinKeyLength} characters. " +
                $"Set '{RecipeManagementOptions.JwtOptions.SectionName}:Key'.");
        }

        var now = timeProvider.GetUtcNow();
        var expires = now.AddMinutes(jwt.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expires.UtcDateTime,
            signingCredentials: credentials);

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), "Bearer", expires);
    }
}
