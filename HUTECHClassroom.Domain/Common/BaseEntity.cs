namespace HUTECHClassroom.Domain.Common;

public abstract class BaseEntity<TKey> : IEntity<TKey>, IAuditableEntity
{
    public TKey Id { get; set; }
    public DateTime CreateDate { get; init; }
}

public abstract class BaseEntity : BaseEntity<Guid>, IEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreateDate = DateTime.UtcNow;
    }
}
