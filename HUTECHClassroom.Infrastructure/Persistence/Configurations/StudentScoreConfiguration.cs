using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class StudentScoreConfiguration : IEntityTypeConfiguration<StudentScore>
{
    public void Configure(EntityTypeBuilder<StudentScore> builder)
    {
        builder
            .HasKey(x => new { x.StudentId, x.ClassroomId, x.ScoreTypeId });
    }
}
