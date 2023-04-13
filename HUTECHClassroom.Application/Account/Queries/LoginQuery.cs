using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Queries
{
    public record LoginQuery(string UserName, string Password) : IRequest<UserDTO>;
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDTO>
    {
        private readonly UserManager<ApplicationUser> _userManger;

        public LoginQueryHandler(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger;
        }
        public async Task<UserDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByNameAsync(request.UserName);

            if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

            var isSuccess = await _userManger.CheckPasswordAsync(user, request.Password);

            if (isSuccess)
                return new UserDTO { UserName = user.UserName, Email = user.Email, Token = "" };

            throw new UnauthorizedAccessException(nameof(ApplicationUser));
        }
    }
}
