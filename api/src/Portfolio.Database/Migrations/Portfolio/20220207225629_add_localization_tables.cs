using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_localization_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LanguageCulture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlagImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocaleStringResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Page = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleStringResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocaleStringResources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "DisplayNumber", "FlagImageFilePath", "IsPublished", "LanguageCulture", "Name" },
                values: new object[] { 1, 0, "languages/united-33135.svg", true, "en-US", "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "DisplayNumber", "FlagImageFilePath", "IsPublished", "LanguageCulture", "Name" },
                values: new object[] { 2, 1, "languages/netherlands-33035.svg", false, "nl-NL", "Dutch" });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUTC",
                value: new DateTime(2022, 2, 7, 22, 56, 28, 650, DateTimeKind.Utc).AddTicks(8235));

            migrationBuilder.CreateIndex(
                name: "IX_LocaleStringResources_LanguageId",
                table: "LocaleStringResources",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocaleStringResources");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAtUTC",
                value: new DateTime(2022, 1, 26, 23, 35, 22, 41, DateTimeKind.Utc).AddTicks(5792));
        }
    }
}
