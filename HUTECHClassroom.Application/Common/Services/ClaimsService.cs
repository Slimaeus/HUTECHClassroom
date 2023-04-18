using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Domain.Entities;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HUTECHClassroom.Application.Common.Services;

public class ClaimsService : IClaimsService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ClaimsService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public void AddClaims(ApplicationRole role, string entity, IList<string> actions = default)
    {
        actions.ForEach(async x =>
        {
            await _roleManager.AddClaimAsync(role, new Claim(entity, x)).ConfigureAwait(false);
        });
    }
}
