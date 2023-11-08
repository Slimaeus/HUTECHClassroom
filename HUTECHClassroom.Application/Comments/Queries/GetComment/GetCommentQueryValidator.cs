namespace HUTECHClassroom.Application.Comments.Queries.GetComment;

public sealed class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
{
    public GetCommentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
