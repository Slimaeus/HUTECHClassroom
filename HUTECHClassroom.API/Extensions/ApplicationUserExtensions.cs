using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HUTECHClassroom.API.Extensions;

public static class ApplicationUserExtensions
{
    public static IList<Claim> GetRoleClaims(this ApplicationUser user, ApplicationDbContext context)
    {
        var roles = context.Roles
            .Where(role => role.ApplicationUserRoles.Any(x => x.UserId == user.Id));

        var roleClaims = context.RoleClaims
            .Where(claim => roles.Select(role => role.Id).Contains(claim.RoleId))
            .Select(x => new Claim(x.ClaimType, x.ClaimValue))
            .Distinct()
            .ToList();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        return roleClaims;
    }
}
