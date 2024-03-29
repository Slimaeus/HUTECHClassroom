﻿using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Content)
            .HasMaxLength(CommentConstants.CONTENT_MAX_LENGTH)
            .IsRequired();
    }
}

