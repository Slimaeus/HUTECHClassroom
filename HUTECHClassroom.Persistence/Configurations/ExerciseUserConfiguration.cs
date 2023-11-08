using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class ExerciseUserConfiguration : IEntityTypeConfiguration<ExerciseUser>
{
    public void Configure(EntityTypeBuilder<ExerciseUser> builder)
    {
        builder.ToTable($"{nameof(ExerciseUser)}s");
        builder.HasKey(x => new { x.UserId, x.ExerciseId });
    }
}
