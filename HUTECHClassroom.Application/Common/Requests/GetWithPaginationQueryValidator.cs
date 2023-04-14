using FluentValidation;

namespace HUTECHClassroom.Application.Common.Requests
{
    public abstract class GetWithPaginationQueryValidator<TQuery, TDTO> : AbstractValidator<TQuery>
        where TQuery : GetWithPaginationQuery<TDTO>
        where TDTO : class
    {
        public GetWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

            RuleFor(x => x.SearchString)
                .MaximumLength(100);
        }
    }
}
