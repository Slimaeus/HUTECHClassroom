namespace HUTECHClassroom.Application.Groups.Queries.GetGroup;

public sealed class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
