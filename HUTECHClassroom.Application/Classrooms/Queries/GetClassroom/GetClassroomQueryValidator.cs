using FluentValidation;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;

public class GetClassroomQueryValidator : AbstractValidator<GetClassroomQuery>
{
    public GetClassroomQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
