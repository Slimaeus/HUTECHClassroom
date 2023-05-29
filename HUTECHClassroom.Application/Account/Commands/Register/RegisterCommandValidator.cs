using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Faculty> _facultyRepository;

    public RegisterCommandValidator(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _facultyRepository = unitOfWork.Repository<Faculty>();

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .MinimumLength(3).WithMessage("UserName must be at least 3 characters long.")
            .MustAsync(IsUniqueUserName).WithMessage("The specified UserName is already in use.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not in the correct format.")
            .MustAsync(IsUniqueEmail).WithMessage("The specified Email is already in use.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is requied.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.FacultyId)
            .MustAsync(ValidateFacultyId).WithMessage("The specified Faculty Id does not exist.");
    }

    private async Task<bool> IsUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _userManager.FindByNameAsync(userName) == null && await _userManager.FindByEmailAsync(userName) == null;
    }

    private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userManager.FindByEmailAsync(email) == null && await _userManager.FindByNameAsync(email) == null; ;
    }

    private async Task<bool> ValidateFacultyId(Guid facultyId, CancellationToken cancellationToken)
    {
        if (facultyId == Guid.Empty)
        {
            return true; // Skip validation if facultyId is not provided
        }

        // There should only one faculty found
        var isFacultyIdValid = await _facultyRepository.CountAsync(x => x.Id == facultyId, cancellationToken) == 1;
        return isFacultyIdValid;
    }
}
