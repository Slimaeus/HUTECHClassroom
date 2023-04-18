﻿using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.Register;

public record RegisterCommand : IRequest<UserDTO>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    //public Guid FacultyId { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManger)
    {
        _userManger = userManger;
    }
    public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
        };
        var result = await _userManger.CreateAsync(user, request.Password);

        if (result.Succeeded) return UserDTO.Create(user);

        throw new InvalidOperationException("Failed to register");
    }
}
