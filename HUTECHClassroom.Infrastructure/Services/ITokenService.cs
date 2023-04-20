using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Infrastructure.Services;
public interface ITokenService
{
    Task<string> CreateToken(ApplicationUser user);
    DateTime GetExpireDate(string token);
}