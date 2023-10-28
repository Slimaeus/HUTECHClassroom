using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public class ClassroomUserConfiguration : IEntityTypeConfiguration<ClassroomUser>
{
    public void Configure(EntityTypeBuilder<ClassroomUser> builder)
    {
        builder.ToTable($"{nameof(ClassroomUser)}s");
        builder.HasKey(x => new { x.UserId, x.ClassroomId });
    }
}
