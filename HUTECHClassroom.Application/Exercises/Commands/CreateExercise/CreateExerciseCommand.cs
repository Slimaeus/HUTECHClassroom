﻿using HUTECHClassroom.Application.Common.Requests;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Exercises.Commands.CreateExercise;

public record CreateExerciseCommand : CreateCommand
{
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string Topic { get; set; }
    public string Criteria { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreateExerciseCommandHandler : CreateCommandHandler<Exercise, CreateExerciseCommand>
{
    private readonly IRepository<Classroom> _classroomRepository;

    public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _classroomRepository = unitOfWork.Repository<Classroom>();
    }

    protected override async Task ValidateAdditionalField(CreateExerciseCommand request, Exercise entity)
    {
        var classroomQuery = _classroomRepository.SingleResultQuery()
            .Include(i => i.Include(c => c.ClassroomUsers))
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _classroomRepository.SingleOrDefaultAsync(classroomQuery);

        foreach (var classroomUser in classroom.ClassroomUsers)
        {
            entity.ExerciseUsers.Add(new ExerciseUser { UserId = classroomUser.UserId });
        }
    }
}
