using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;

public sealed class UpdateSubjectCommandValidator : UpdateCommandValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.Code).MaximumLength(SubjectConstants.CODE_MAX_LENGTH);
        RuleFor(x => x.Title).MaximumLength(SubjectConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
    }
}
