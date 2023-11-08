using HUTECHClassroom.Application.Account.DTOs;

namespace HUTECHClassroom.Application.Common.Extensions;

public static class CreateDTOExtensions
{
    public static AccountDTO ToAccountDTO(this ApplicationUser user, string? token)
        => new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token,
            Faculty = user.Faculty != null ? new UserFacultyDTO { Id = user.Faculty.Id, Name = user.Faculty.Name } : null,
            Roles = user.ApplicationUserRoles?.Where(x => x.Role is { Name: { } }).Select(x => x.Role.Name!) ?? Enumerable.Empty<string>(),
        };
}
