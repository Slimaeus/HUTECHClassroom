﻿using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(100);

        builder.Property(x => x.Room)
            .HasMaxLength(50);

        builder.Property(x => x.Topic)
            .HasMaxLength(20);

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
