namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record StudentResultWithOrdinalDTO(int? OrdinalNumber, string? Id, double? Score)
{
    public StudentResultWithOrdinalDTO() : this(null, null, null) { }
}
