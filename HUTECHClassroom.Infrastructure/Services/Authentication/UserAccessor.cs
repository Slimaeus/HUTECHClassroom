using HUTECHClassroom.Domain.Claims;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;
using System.Security.Claims;

namespace HUTECHClassroom.Infrastructure.Services.Authentication;

public sealed class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;
    public Guid Id => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
    public string UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
    public string Jwt
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null) return string.Empty;
            var hasCookieToken = httpContext.Request.Cookies.TryGetValue(AuthenticationConstants.CookieAccessToken, out var cookieToken);
            if (hasCookieToken && cookieToken is { })
            {
                return cookieToken;
            }
            var hasHeaderToken = httpContext.Request.Headers.TryGetValue("Authorization", out var authoriaztionHeader);
            if (hasHeaderToken && authoriaztionHeader is { })
            {
                return authoriaztionHeader
                  .ToString()
                  .Replace("Bearer ", string.Empty);
            }
            var hasQueryToken = httpContext.Request.Query.TryGetValue(AuthenticationConstants.WebSocketAccessToken, out var accessToken);
            if (hasQueryToken && accessToken is { })
            {
                return accessToken.ToString();
            }
            return string.Empty;
        }
    }
    public IList<string> Roles => _httpContextAccessor.HttpContext?.User.Claims
        .Where(x => x.Type == ClaimTypes.Role)
        .Select(x => x.Value)
        .ToList() ?? [];
    public IDictionary<string, ImmutableArray<string>> EntityClaims => _httpContextAccessor.HttpContext?.User.Claims
        .Where(x => ApplicationClaimTypes.EntityClaimTypes.Contains(x.Type))
        .GroupBy(x => x.Type)
        .ToDictionary(g => g.Key, g => g.Select(x => x.Value).ToImmutableArray()) ?? [];

    public void AppendCookieAccessToken(string token)
        => _httpContextAccessor.HttpContext?.Response.Cookies
            .Append(AuthenticationConstants.CookieAccessToken, token);
}

