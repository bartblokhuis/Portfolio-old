using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class blog_settings_add_subject_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOnPublishingSubjectTemplate",
                table: "BlogSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailOnSubscribingSubjectTemplate",
                table: "BlogSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOnPublishingSubjectTemplate",
                table: "BlogSettings");

            migrationBuilder.DropColumn(
                name: "EmailOnSubscribingSubjectTemplate",
                table: "BlogSettings");
        }
    }
}
