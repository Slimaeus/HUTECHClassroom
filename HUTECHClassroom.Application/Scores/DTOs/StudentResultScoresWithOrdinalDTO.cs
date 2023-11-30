namespace HUTECHClassroom.Application.Scores.DTOs;

public sealed record StudentResultScoresWithOrdinalDTO(int? OrdinalNumber, string? Id, double? Score1, double? Score2)
{
    public StudentResultScoresWithOrdinalDTO() : this(null, null, null, null) { }
}
