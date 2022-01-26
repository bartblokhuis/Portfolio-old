using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using System;

namespace Portfolio.Database.Configurations;

internal class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(nameof(Message.FirstName)).HasMaxLength(64);
        builder.Property(nameof(Message.LastName)).HasMaxLength(64);
        builder.Property(nameof(Message.MessageContent)).HasMaxLength(512);
        builder.Property(nameof(Message.Email)).HasMaxLength(128);

        //IPv6 addresses have a max length of 45
        builder.Property(nameof(Message.IpAddress)).HasMaxLength(45);

        builder.HasData(new Message { Id = 1, FirstName = "Bart", MessageContent = "This is an example message", MessageStatus = MessageStatus.Unread, IsDeleted = false, CreatedAtUTC = DateTime.UtcNow, Email = "info@bartblokhuis.com" });
    }
}
