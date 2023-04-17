using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination
{
    public class GetGroupProjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupProjectsWithPaginationQuery, GroupProjectDTO>
    {
        public GetGroupProjectsWithPaginationQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
