using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Validators;

public class EntityExistenceByNotNullIdValidator<TKey, TField, TEntity> : EntityExistenceByIdValidator<TKey, TField, TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
    where TField : notnull
{
    public EntityExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}

public class EntityExistenceByNotNullIdValidator<TEntity> : EntityExistenceByNotNullIdValidator<Guid, Guid, TEntity>
    where TEntity : class, IEntity<Guid>
{
    public EntityExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork) { }
}