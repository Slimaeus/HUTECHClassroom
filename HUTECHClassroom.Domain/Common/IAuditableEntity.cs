namespace HUTECHClassroom.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreateDate { get; init; }

    DateTime? UpdateDate { get; set; }
}
