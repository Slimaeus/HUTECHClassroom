using HUTECHClassroom.Application.Common.Models;

namespace HUTECHClassroom.Application.Common.Validators;

public sealed class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
        .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.SearchString)
            .MaximumLength(100);
    }
}
