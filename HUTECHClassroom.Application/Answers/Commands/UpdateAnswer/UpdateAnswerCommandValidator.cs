using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;

public class UpdateAnswerCommandValidator : UpdateCommandValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(AnswerConstants.DESCRIPTION_MAX_LENGTH);

        RuleFor(x => x.Link)
            .MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.Score);
    }
}
