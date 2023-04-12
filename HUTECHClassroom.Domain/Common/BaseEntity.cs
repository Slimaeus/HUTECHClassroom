namespace HUTECHClassroom.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; init; } = default;
        public DateTime CreateDate { get; init; } = default;
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
    }
}
