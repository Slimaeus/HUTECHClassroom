using HUTECHClassroom.Domain.Claims;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;
using System.Security.Claims;

namespace HUTECHClassroom.Infrastructure.Services;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid Id => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
    public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;
    public IList<string> Roles => _httpContextAccessor.HttpContext.User.Claims
        .Where(x => x.Type == ClaimTypes.Role)
        .Select(x => x.Value)
        .ToList();
    public IDictionary<string, ImmutableArray<string>> EntityClaims => _httpContextAccessor.HttpContext.User.Claims
        .Where(x => ApplicationClaimTypes.EntityClaimTypes.Contains(x.Type))
        .GroupBy(x => x.Type)
        .ToDictionary(g => g.Key, g => g.Select(x => x.Value).ToImmutableArray());
}

