using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Domain.Interfaces;
public interface ITokenService
{
    string CreateToken(ApplicationUser user);
    DateTime GetExpireDate(string token);
}