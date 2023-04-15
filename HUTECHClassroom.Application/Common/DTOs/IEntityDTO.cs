namespace HUTECHClassroom.Application.Common.DTOs
{
    public interface IEntityDTO<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntityDTO : IEntityDTO<Guid> { }
}
