using EntityFrameworkCore.Triggered;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Infrastructure.Persistence.Triggers;
public sealed class AfterCreateExerciseTrigger : IAfterSaveTrigger<Exercise>
{
    private readonly ApplicationDbContext _dbContext;

    public AfterCreateExerciseTrigger(ApplicationDbContext dbContext)
        => _dbContext = dbContext;
    public async Task AfterSave(ITriggerContext<Exercise> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType != ChangeType.Added) return;

        var exercise = context.Entity;

        var classroom = await _dbContext
            .Classrooms
            .Include(c => c.ClassroomUsers)
            .SingleOrDefaultAsync(
                x => x.Id == exercise.ClassroomId,
                cancellationToken: cancellationToken);

        foreach (var classroomUser in classroom.ClassroomUsers)
        {
            exercise.ExerciseUsers
                .Add(new ExerciseUser
                {
                    UserId = classroomUser.UserId
                });
        }

        _dbContext.Update(exercise);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
