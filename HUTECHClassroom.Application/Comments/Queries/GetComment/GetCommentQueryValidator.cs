using FluentValidation;

namespace HUTECHClassroom.Application.Comments.Queries.GetComment;

public class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
{
    public GetCommentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
