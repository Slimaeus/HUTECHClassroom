using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace HUTECHClassroom.Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDTO>
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? ClassId { get; set; }
}
public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IUserAccessor _userAccessor;
    private readonly IRepository<Faculty> _facultyRepository;
    private readonly IRepository<Class> _classRepository;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper, IMemoryCache memoryCache, IUserAccessor userAccessor)
    {
        _userManger = userManger;
        _tokenService = tokenService;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _userAccessor = userAccessor;
        _facultyRepository = unitOfWork.Repository<Faculty>();
        _classRepository = unitOfWork.Repository<Class>();
    }
    public async Task<AccountDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        if (request.FacultyId != Guid.Empty)
        {
            var facultyQuery = _facultyRepository
                .SingleResultQuery()
                .AndFilter(x => x.Id == request.FacultyId);

            var faculty = await _facultyRepository.SingleOrDefaultAsync(facultyQuery, cancellationToken);

            if (faculty is null)
            {
                user.Faculty = faculty;
            }
        }

        if (Guid.Empty != request.ClassId)
        {
            var classQuery = _classRepository
                .SingleResultQuery()
                .AndFilter(x => x.Id == request.ClassId);

            var @class = await _classRepository.SingleOrDefaultAsync(classQuery, cancellationToken);

            if (@class is null)
            {
                user.Class = @class;
            }
        }

        var result = await _userManger.CreateAsync(user, request.Password).ConfigureAwait(false);
        await _userManger.AddToRoleAsync(user, RoleConstants.Student);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to register");
        }

        var accountDTO = _mapper.Map<AccountDTO>(user);
        var token = _tokenService.CreateToken(user);
        _userAccessor.AppendCookieAccessToken(token);
        _memoryCache.Set($"UserToken_{user.UserName}", token);
        accountDTO.Token = token;
        return accountDTO;
    }
}
