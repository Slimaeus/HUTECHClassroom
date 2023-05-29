using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDTO>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public Guid FacultyId { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDTO>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IRepository<ApplicationUser> _userRepository;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _userManger = userManger;
        _tokenService = tokenService;
        _mapper = mapper;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
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

            user.FacultyId = request.FacultyId;
        }

        var result = await _userManger.CreateAsync(user, request.Password);

        var query = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == user.Id);

        var accountDTO = await _userRepository
            .ToQueryable(query)
            .ProjectTo<AccountDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        accountDTO.Token = _tokenService.CreateToken(user);

        if (result.Succeeded) return accountDTO;

        throw new InvalidOperationException("Failed to register");
    }
}
