using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HUTECHClassroom.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly string _tokenKey;
    private readonly TimeSpan _tokenLifespan;
    private readonly SigningCredentials _signingCredentials;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public TokenService(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _tokenKey = configuration["TokenKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));
        _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        _tokenLifespan = TimeSpan.FromMinutes(10);
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> CreateToken(ApplicationUser user)
    {
        var roleNames = await _userManager.GetRolesAsync(user);

        var roles = _roleManager.Roles
            .Where(role => roleNames.Contains(role.Name));

        var roleClaims = _context.RoleClaims
            .Where(claim => roles.Select(role => role.Id).Contains(claim.RoleId))
            .Select(x => new Claim(x.ClaimType, x.ClaimValue))
            .Distinct();

        var tokenClaims = new List<Claim>
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
        };

        tokenClaims.AddRange(roleClaims);

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
        if (token == null)
            return DateTime.Now;
        return jwtToken.ValidTo.ToUniversalTime();
    }
}
