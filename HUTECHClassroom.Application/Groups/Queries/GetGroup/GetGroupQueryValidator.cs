namespace HUTECHClassroom.Application.Groups.Queries.GetGroup;

public class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
