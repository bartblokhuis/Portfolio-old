using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_message_settings_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSendSiteOwnerEmail = table.Column<bool>(type: "bit", nullable: false),
                    SiteOwnerSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteOwnerTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSendConfirmationEmail = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmationEmailSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationEmailTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageSettings");
        }
    }
}
