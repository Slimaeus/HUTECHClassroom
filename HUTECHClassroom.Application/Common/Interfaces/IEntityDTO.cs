namespace HUTECHClassroom.Application.Common.Interfaces
{
    public interface IEntityDTO<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntityDTO : IEntityDTO<Guid> { }
}
