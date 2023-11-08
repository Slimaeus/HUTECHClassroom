using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HUTECHClassroom.Infrastructure.Services.Authentication;

public sealed class TokenService : ITokenService
{
    private readonly string _tokenKey;
    private readonly TimeSpan _tokenLifespan;
    private readonly SigningCredentials _signingCredentials;
    private readonly ApplicationDbContext _context;

    public TokenService(IConfiguration configuration, ApplicationDbContext context)
    {
        _tokenKey = configuration[ServiceConstants.TOKEN_KEY];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));
        _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        _tokenLifespan = TimeSpan.FromHours(5);
        _context = context;
    }

    public string CreateToken(ApplicationUser user)
    {
        var tokenClaims = new List<Claim>
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
        };

        tokenClaims.AddRange(GetRoleClaims(user));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(tokenClaims),
            Expires = DateTime.Now.Add(_tokenLifespan),
            SigningCredentials = _signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public DateTime GetExpireDate(string token)
    {
        JwtSecurityToken jwtToken = new(token);
        if (token is null)
            return DateTime.Now;
        return jwtToken.ValidTo.ToUniversalTime();
    }

    private IList<Claim> GetRoleClaims(ApplicationUser user)
    {
        var roles = _context.Roles
            .Where(role => role.ApplicationUserRoles.Any(x => x.UserId == user.Id));

        var roleClaims = _context.RoleClaims
            .Where(claim => roles.Select(role => role.Id).Contains(claim.RoleId))
            .Select(x => x.ToClaim())
            .Distinct()
            .ToList();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        return roleClaims;
    }
}
