using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_project_image_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectPictures_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPictures_PictureId",
                table: "ProjectPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPictures_ProjectId",
                table: "ProjectPictures",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectPictures");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
