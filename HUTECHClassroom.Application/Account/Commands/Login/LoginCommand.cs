using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Account.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<AccountDTO>;
public class LoginCommandHandler : IRequestHandler<LoginCommand, AccountDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManger, ITokenService tokenService)
    {
        _userManger = userManger;
        _tokenService = tokenService;
    }
    public async Task<AccountDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManger
            .Users
            .Include(x => x.Faculty)
            .Include(x => x.ApplicationUserRoles)
            .ThenInclude(x => x.Role)
            .SingleOrDefaultAsync(x => x.UserName == request.UserName);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var isSuccess = await _userManger.CheckPasswordAsync(user, request.Password);

        if (isSuccess) return AccountDTO.Create(user, _tokenService.CreateToken(user));

        throw new UnauthorizedAccessException(nameof(ApplicationUser));
    }
}
