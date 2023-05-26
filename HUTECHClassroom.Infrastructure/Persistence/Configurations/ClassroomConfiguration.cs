using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(ClassroomConstants.TITLE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(ClassroomConstants.DESCRIPTION_MAX_LENGTH);

        builder.Property(x => x.Room)
            .HasMaxLength(ClassroomConstants.ROOM_MAX_LENGTH);

        builder.Property(x => x.StudyPeriod)
            .HasMaxLength(ClassroomConstants.STUDY_PERIOD_MAX_LENGTH);

        builder.Property(x => x.Topic)
            .HasMaxLength(ClassroomConstants.TOPIC_MAX_LENGTH);

        builder.Property(x => x.SchoolYear)
            .HasMaxLength(ClassroomConstants.SCHOOL_YEAR_MAX_LENGTH);

        builder.Property(x => x.StudyGroup)
            .HasMaxLength(ClassroomConstants.STUDY_GROUP_MAX_LENGTH);

        builder.Property(x => x.PracticalStudyGroup)
            .HasMaxLength(ClassroomConstants.PRACTIAL_STUDY_GROUP_MAX_LENGTH);

        builder.HasMany(x => x.Groups)
            .WithOne(x => x.Classroom)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Excercises)
            .WithOne(x => x.Classroom)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Classroom)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
