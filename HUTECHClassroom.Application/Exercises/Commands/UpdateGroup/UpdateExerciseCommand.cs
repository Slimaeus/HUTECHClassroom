using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;

public record UpdateExerciseCommand(Guid Id) : UpdateCommand(Id)
{
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string Topic { get; set; }
    public string Criteria { get; set; }
}
public class UpdateExerciseCommandHandler : UpdateCommandHandler<Exercise, UpdateExerciseCommand>
{
    public UpdateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
