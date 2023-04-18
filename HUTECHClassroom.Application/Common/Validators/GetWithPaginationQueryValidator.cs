using FluentValidation;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public abstract class GetWithPaginationQueryValidator<TQuery, TDTO> : AbstractValidator<TQuery>
    where TQuery : GetWithPaginationQuery<TDTO>
    where TDTO : class
{
    public GetWithPaginationQueryValidator()
    {
        RuleFor(x => x.Params).SetValidator(new PaginationParamsValidator());
    }
}
