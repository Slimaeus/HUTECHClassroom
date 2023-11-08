namespace HUTECHClassroom.Application.Common.Validators.Posts;

public sealed class PostExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Post>
{
    public PostExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
