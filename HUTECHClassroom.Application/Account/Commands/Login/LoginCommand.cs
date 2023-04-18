using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        var user = await _userManger.FindByNameAsync(request.UserName);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var isSuccess = await _userManger.CheckPasswordAsync(user, request.Password);

        if (isSuccess) return UserDTO.Create(user);

        throw new UnauthorizedAccessException(nameof(ApplicationUser));
    }
}
