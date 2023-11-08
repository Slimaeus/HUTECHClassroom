namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;

public sealed class GetClassroomQueryValidator : AbstractValidator<GetClassroomQuery>
{
    public GetClassroomQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
