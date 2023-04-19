using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;

public class UpdateAnswerCommandValidator : UpdateCommandValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Link)
            .MaximumLength(200);

        RuleFor(x => x.Score);
    }
}
