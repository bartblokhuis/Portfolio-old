using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Database.Configurations;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(t => t.Content)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(t => t.Email)
            .HasMaxLength(128);
    }
}
