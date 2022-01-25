using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_scheduled_tasks_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    StopOnError = table.Column<bool>(type: "bit", nullable: false),
                    LastStartUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastEndUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSuccessUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleTasks");
        }
    }
}
