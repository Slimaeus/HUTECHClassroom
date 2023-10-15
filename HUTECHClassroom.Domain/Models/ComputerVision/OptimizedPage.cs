namespace HUTECHClassroom.Domain.Models.ComputerVision;

public sealed record OptimizedPage(int Page, IEnumerable<OptimizedLine> OptimizedLines);