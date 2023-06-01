using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDTO>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public Guid? FacultyId { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IRepository<Faculty> _facultyRepository;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _userManger = userManger;
        _tokenService = tokenService;
        _mapper = mapper;
        _facultyRepository = unitOfWork.Repository<Faculty>();
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

            if (faculty != null)
            {
                user.Faculty = faculty;
            }
        }

        var result = await _userManger.CreateAsync(user, request.Password).ConfigureAwait(false);
        await _userManger.AddToRoleAsync(user, "Student");

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to register");
        }

        var accountDTO = _mapper.Map<AccountDTO>(user);
        var token = _tokenService.CreateToken(user);
        await _userManger.SetAuthenticationTokenAsync(user, "HUTECHClassroom", "JwtToken", token);
        accountDTO.Token = token;
        return accountDTO;
    }
}
