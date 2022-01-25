using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_default_schedule_task_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ScheduleTasks",
                columns: new[] { "Id", "Enabled", "LastEndUtc", "LastStartUtc", "LastSuccessUtc", "Name", "Seconds", "StopOnError", "Type" },
                values: new object[] { 1, true, null, null, null, "Keep alive", 300, false, "Portfolio.Core.Services.Common.KeepAliveTask, Portfolio.Core" });

            migrationBuilder.InsertData(
                table: "ScheduleTasks",
                columns: new[] { "Id", "Enabled", "LastEndUtc", "LastStartUtc", "LastSuccessUtc", "Name", "Seconds", "StopOnError", "Type" },
                values: new object[] { 2, true, null, null, null, "Send queued emails", 30, false, "Portfolio.Core.Services.Common.QueuedMessagesSendTask, Portfolio.Core" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScheduleTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScheduleTasks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
