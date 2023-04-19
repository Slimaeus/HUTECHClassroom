using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Infrastructure.Services;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;
}

