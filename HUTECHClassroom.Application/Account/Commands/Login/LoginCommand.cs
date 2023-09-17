using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HUTECHClassroom.Application.Account.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<AccountDTO>;
public class LoginCommandHandler : IRequestHandler<LoginCommand, AccountDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IRepository<ApplicationUser> _userRepository;

    public LoginCommandHandler(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper, IMemoryCache memoryCache)
    {
        _userManger = userManger;
        _tokenService = tokenService;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<AccountDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Faculty))
            .Include(i => i.Include(x => x.ApplicationUserRoles).ThenInclude(x => x.Role))
            .Include(i => i.Include(x => x.Avatar))
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken).ConfigureAwait(false)
            ?? throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var isSuccess = await _userManger
            .CheckPasswordAsync(user, request.Password).ConfigureAwait(false);

        if (!isSuccess)
            throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var accountDTO = _mapper
            .Map<AccountDTO>(user);


        var doesGetCacheTokenSuccess = _memoryCache
            .TryGetValue($"UserToken_{user.UserName}", out string memoryCacheToken);

        if (doesGetCacheTokenSuccess)
        {
            var expireDate = _tokenService.GetExpireDate(memoryCacheToken);
            if (expireDate >= DateTime.Now.ToUniversalTime().AddMinutes(10))
            {
                accountDTO.Token = memoryCacheToken;
                return accountDTO;
            }
            _memoryCache.Remove($"UserToken_{user.UserName}");
        }

        var token = _tokenService.CreateToken(user);
        accountDTO.Token = token;
        _memoryCache.Set($"UserToken_{user.UserName}", token);

        return accountDTO;

    }
}
