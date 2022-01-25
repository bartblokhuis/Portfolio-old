using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;

namespace Portfolio.Database.Configurations;

internal class ScheduleTaskConfiguration : IEntityTypeConfiguration<ScheduleTask>
{
    public void Configure(EntityTypeBuilder<ScheduleTask> builder)
    {
        builder.HasData(
            new ScheduleTask { Id = 1, Name = "Keep alive", Seconds = 300, Type = "Portfolio.Core.Services.Common.KeepAliveTask, Portfolio.Core", Enabled = true, StopOnError = false },
            new ScheduleTask { Id = 2, Name = "Send queued emails", Seconds = 30, Type = "Portfolio.Core.Services.Common.QueuedMessagesSendTask, Portfolio.Core", Enabled = true, StopOnError = false });
    }
}
