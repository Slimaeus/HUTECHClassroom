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
    private readonly IMapper _mapper;
    private readonly IRepository<ApplicationUser> _userRepository;

    public LoginCommandHandler(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _userManger = userManger;
        _tokenService = tokenService;
        _mapper = mapper;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<AccountDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Faculty))
            .Include(i => i.Include(x => x.ApplicationUserRoles).ThenInclude(x => x.Role))
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken).ConfigureAwait(false);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var isSuccess = await _userManger.CheckPasswordAsync(user, request.Password).ConfigureAwait(false);

        var accountDTO = _mapper.Map<AccountDTO>(user);

        if (!isSuccess)
        {
            throw new UnauthorizedAccessException(nameof(ApplicationUser));
        }

        var cacheToken = await _userManger.GetAuthenticationTokenAsync(user, "HUTECHClassroom", "JwtToken").ConfigureAwait(false);

        if (cacheToken != null)
        {
            var expireDate = _tokenService.GetExpireDate(cacheToken);
            if (expireDate >= DateTime.Now.ToUniversalTime().AddMinutes(10))
            {
                accountDTO.Token = cacheToken;
                return accountDTO;
            }
            await _userManger.RemoveAuthenticationTokenAsync(user, "HUTECHClassroom", "JwtToken").ConfigureAwait(false);
        }

        var token = _tokenService.CreateToken(user);
        accountDTO.Token = token;
        await _userManger.SetAuthenticationTokenAsync(user, "HUTECHClassroom", "JwtToken", token).ConfigureAwait(false);

        return accountDTO;

    }
}
