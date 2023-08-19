namespace HUTECHClassroom.Domain.Common;

public record PhotoUploadResult
{
    public required string PublicId { get; set; }
    public required string Url { get; set; }
}
