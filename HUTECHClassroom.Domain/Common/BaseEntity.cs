namespace HUTECHClassroom.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }

    public abstract class BaseEntity : BaseEntity<Guid> { }
}
