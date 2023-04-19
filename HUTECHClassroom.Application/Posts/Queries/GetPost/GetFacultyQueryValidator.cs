using FluentValidation;

namespace HUTECHClassroom.Application.Posts.Queries.GetPost;

public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
