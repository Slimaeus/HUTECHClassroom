using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Validators;

public class EntityExistenceByIdValidator<TKey, TEntity> : AbstractValidator<TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly IRepository<TEntity> _repository;

    public EntityExistenceByIdValidator(IUnitOfWork unitOfWork, string message)
    {
        _repository = unitOfWork.Repository<TEntity>();

        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateUserId).WithMessage(message);
    }

    private async Task<bool> ValidateUserId(TKey id, CancellationToken cancellationToken)
    {
        if (id.Equals(default(TKey)))
        {
            return true;
        }

        var isUserIdValid = await _repository.CountAsync(x => x.Id.Equals(id), cancellationToken) == 1;
        return isUserIdValid;
    }
}

public class EntityExistenceByIdValidator<TEntity> : EntityExistenceByIdValidator<Guid, TEntity>
    where TEntity : class, IEntity<Guid>
{
    public EntityExistenceByIdValidator(IUnitOfWork unitOfWork, string message) : base(unitOfWork, message)
    {
    }
}