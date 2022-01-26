using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class SkillGroupConfiguration : IEntityTypeConfiguration<SkillGroup>
{
    public void Configure(EntityTypeBuilder<SkillGroup> builder)
    {
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(t => t.Title)
            .IsUnique(true);
    }
}