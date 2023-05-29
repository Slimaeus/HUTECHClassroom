using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Validators;

public class EntityExistenceByIdValidator<TKey, TField, TEntity> : AbstractValidator<TField>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    private readonly IRepository<TEntity> _repository;
    public EntityExistenceByIdValidator(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.Repository<TEntity>();

        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ValidateId).WithMessage($"The specified {typeof(TEntity).Name} Id does not exist.");
    }

    private async Task<bool> ValidateId(TField id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return true;
        }

        var isIdValid = await _repository.CountAsync(x => x.Id.Equals(id), cancellationToken) == 1;
        return isIdValid;
    }
}

public class EntityExistenceByIdValidator<TEntity> : EntityExistenceByIdValidator<Guid, Guid?, TEntity>
    where TEntity : class, IEntity<Guid>
{
    public EntityExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
