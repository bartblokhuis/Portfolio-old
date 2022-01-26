using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(t => t.Title)
            .IsUnique(true);

        builder.Property(t => t.Description)
            .HasMaxLength(512);

        builder.HasData(new Project { 
            Id = 1,
            Title = "Example Project", 
            Description = "This is an example project", 
            IsPublished = true });
    }
}