using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Database.Configurations;

internal class BlogSubscriberConfiguration : IEntityTypeConfiguration<BlogSubscriber>
{
    public void Configure(EntityTypeBuilder<BlogSubscriber> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(64);

        builder.HasIndex(t => t.EmailAddress)
            .IsUnique(true);

        builder.Property(t => t.EmailAddress)
            .IsRequired()
            .HasMaxLength(128);
    }
}