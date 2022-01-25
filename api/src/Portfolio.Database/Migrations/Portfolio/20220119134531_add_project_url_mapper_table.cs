using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_project_url_mapper_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls");

            migrationBuilder.DropIndex(
                name: "IX_Urls_ProjectId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Urls");

            migrationBuilder.CreateTable(
                name: "ProjectUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UrlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUrls_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUrls_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUrls_ProjectId",
                table: "ProjectUrls",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUrls_UrlId",
                table: "ProjectUrls",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUrls");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Urls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ProjectId",
                table: "Urls",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
