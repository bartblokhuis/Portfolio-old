using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.Property(nameof(Picture.Path)).HasMaxLength(256).IsRequired();
        builder.Property(nameof(Picture.AltAttribute)).HasMaxLength(256);
        builder.Property(nameof(Picture.TitleAttribute)).HasMaxLength(512);
    }
}