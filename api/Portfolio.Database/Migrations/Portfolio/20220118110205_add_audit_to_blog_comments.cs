using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_audit_to_blog_comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUTC",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUTC",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "Comments");
        }
    }
}
