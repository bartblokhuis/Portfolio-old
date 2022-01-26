using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Database.Configurations;

internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasIndex(t => t.Title)
            .IsUnique(true);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(t => t.MetaDescription)
            .HasMaxLength(256);

        builder.Property(t => t.MetaTitle)
            .HasMaxLength(256);
    }
}