using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().MaximumLength(SubjectConstants.CODE_MAX_LENGTH);
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(SubjectConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
    }
}
