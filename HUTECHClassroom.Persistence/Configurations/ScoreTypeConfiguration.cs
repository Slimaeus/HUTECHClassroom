using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class ScoreTypeConfiguration : IEntityTypeConfiguration<ScoreType>
{
    public void Configure(EntityTypeBuilder<ScoreType> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
