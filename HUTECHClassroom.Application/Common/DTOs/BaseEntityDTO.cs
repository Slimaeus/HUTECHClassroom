namespace HUTECHClassroom.Application.Common.DTOs;

public abstract record BaseEntityDTO<TKey> : IEntityDTO<TKey>
{
    public TKey Id { get; set; }
}

public abstract record BaseEntityDTO : BaseEntityDTO<Guid>, IEntityDTO { }
