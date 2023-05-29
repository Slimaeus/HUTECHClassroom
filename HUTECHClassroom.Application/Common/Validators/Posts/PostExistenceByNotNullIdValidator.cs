namespace HUTECHClassroom.Application.Common.Validators.Posts;

public class PostExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Post>
{
    public PostExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
