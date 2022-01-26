using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class ScheduleTaskConfiguration : IEntityTypeConfiguration<ScheduleTask>
{
    public void Configure(EntityTypeBuilder<ScheduleTask> builder)
    {
        builder.Property(t => t.Type)
            .IsRequired();

        builder.HasIndex(t => t.Type)
            .IsUnique(true);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasData(
            new ScheduleTask { Id = 1, Name = "Keep alive", Seconds = 300, Type = "Portfolio.Services.Common.KeepAliveTask, Portfolio.Services", Enabled = true, StopOnError = false },
            new ScheduleTask { Id = 2, Name = "Send queued emails", Seconds = 30, Type = "Portfolio.Services.Common.QueuedMessagesSendTask, Portfolio.Services", Enabled = true, StopOnError = false });
    }
}
