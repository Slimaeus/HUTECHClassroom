namespace HUTECHClassroom.Application.Common.Validators.Posts;

public class PostExistenceByIdValidator : EntityExistenceByIdValidator<Post>
{
    public PostExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
