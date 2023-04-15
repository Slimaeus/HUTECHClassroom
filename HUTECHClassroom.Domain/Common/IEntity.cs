namespace HUTECHClassroom.Domain.Common
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity : IEntity<Guid> { }
}
