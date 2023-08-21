using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HUTECHClassroom.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<AccountDTO>;
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, AccountDTO>
{
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMemoryCache _memoryCache;
    private readonly IRepository<ApplicationUser> _userRepository;

    public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, ITokenService tokenService, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _userAccessor = userAccessor;
        _tokenService = tokenService;
        _userManager = userManager;
        _memoryCache = memoryCache;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }

    public async Task<AccountDTO> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Faculty))
            .Include(i => i.Include(x => x.ApplicationUserRoles).ThenInclude(x => x.Role))
            .AndFilter(x => x.Id == _userAccessor.Id);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var accountDTO = _mapper.Map<AccountDTO>(user);

        var doesGetCacheTokenSuccess = _memoryCache.TryGetValue($"UserToken_{user.UserName}", out string memoryCacheToken);

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