namespace HUTECHClassroom.Application.Common.DTOs;

public interface IEntityDTO<TKey>
{
    TKey? Id { get; init; }
}

public interface IEntityDTO : IEntityDTO<Guid> { }
