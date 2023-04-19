using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Account.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<UserDTO>;
public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;

    public LoginCommandHandler(UserManager<ApplicationUser> userManger)
    {
        _userManger = userManger;
    }
    public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManger
            .Users
            .Include(x => x.Faculty)
            .SingleOrDefaultAsync(x => x.UserName == request.UserName);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var isSuccess = await _userManger.CheckPasswordAsync(user, request.Password);

        if (isSuccess) return UserDTO.Create(user);

        throw new UnauthorizedAccessException(nameof(ApplicationUser));
    }
}
