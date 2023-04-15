namespace HUTECHClassroom.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreateDate { get; init; }
    }
}
