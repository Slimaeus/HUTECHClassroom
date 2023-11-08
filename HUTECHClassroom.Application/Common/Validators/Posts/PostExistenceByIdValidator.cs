namespace HUTECHClassroom.Application.Common.Validators.Posts;

public sealed class PostExistenceByIdValidator : EntityExistenceByIdValidator<Post>
{
    public PostExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
