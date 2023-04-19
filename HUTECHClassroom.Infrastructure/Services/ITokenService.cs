using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Infrastructure.Services;
public interface ITokenService
{
    string CreateToken(ApplicationUser user);
    DateTime GetExpireDate(string token);
}