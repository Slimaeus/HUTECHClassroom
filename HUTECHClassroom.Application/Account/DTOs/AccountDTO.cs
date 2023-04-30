﻿using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Account.DTOs;

public record AccountDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserFacultyDTO Faculty { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string Token { get; set; }

    public static AccountDTO Create(ApplicationUser user, string token = default)
        => new AccountDTO
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Token = token,
            Faculty = new UserFacultyDTO { Id = user.Faculty.Id, Name = user.Faculty.Name },
            Roles = user.ApplicationUserRoles.Select(x => x.Role.Name)
        };
}