namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUser;

public class GetGroupUserQueryValidator : AbstractValidator<GetGroupUserQuery>
{
    public GetGroupUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
