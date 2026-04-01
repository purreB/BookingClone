using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingClone.Application.Abstractions.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookingClone.Infrastructure.Auth;

public sealed class JwtTokenService(IOptions<JwtOptions> jwtOptions) : ITokenService
{
    public Task<TokenResult> CreateTokenAsync(
        TokenPayload payload,
        CancellationToken cancellationToken)
    {
        var options = jwtOptions.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, payload.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, payload.Email),
            new(JwtRegisteredClaimNames.Name, payload.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, payload.UserId.ToString()),
            new(ClaimTypes.Email, payload.Email)
        };

        claims.AddRange(payload.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(options.AccessTokenMinutes);
        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: creds);

        return Task.FromResult(new TokenResult(new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc));
    }
}
