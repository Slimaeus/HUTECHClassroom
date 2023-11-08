namespace HUTECHClassroom.Application.Posts.Queries.GetPost;

public sealed class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
