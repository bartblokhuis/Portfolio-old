using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class QueuedEmailConfiguration : IEntityTypeConfiguration<QueuedEmail>
{
    public void Configure(EntityTypeBuilder<QueuedEmail> builder)
    {
        builder.Property(t => t.From)
            .HasMaxLength(128);
        builder.Property(t => t.FromName)
            .HasMaxLength(128);
        builder.Property(t => t.Body);
        builder.Property(t => t.Subject)
            .HasMaxLength(128);
        builder.Property(t => t.To)
            .HasMaxLength(128);
        builder.Property(t => t.ToName)
            .HasMaxLength(128);

    }
}