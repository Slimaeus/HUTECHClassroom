using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Common.Validators;

public abstract class GetWithPaginationQueryValidator<TQuery, TDTO, TPaginationParams> : AbstractValidator<TQuery>
    where TQuery : GetWithPaginationQuery<TDTO, TPaginationParams>
    where TDTO : class
    where TPaginationParams : PaginationParams
{
    public GetWithPaginationQueryValidator()
    {
        RuleFor(x => x.Params).SetValidator(new PaginationParamsValidator());
    }
}
