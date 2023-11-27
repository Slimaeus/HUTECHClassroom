using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.DTOs;

public abstract record BaseEntityDTO<TKey> : IEntityDTO<TKey>, IAuditableEntity
{
    public BaseEntityDTO()
    {

    }
    public BaseEntityDTO(TKey id)
        => Id = id;
    public TKey? Id { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime? UpdateDate { get; set; } = null;
}

public abstract record BaseEntityDTO : BaseEntityDTO<Guid>, IEntityDTO { }
