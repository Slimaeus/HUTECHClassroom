using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public class StudentResultConfiguration : IEntityTypeConfiguration<StudentResult>
{
    public void Configure(EntityTypeBuilder<StudentResult> builder)
    {
        builder.ToTable($"{nameof(StudentResult)}s");
        builder.HasKey(x => new { x.StudentId, x.ClassroomId, x.ScoreTypeId });
    }
}
