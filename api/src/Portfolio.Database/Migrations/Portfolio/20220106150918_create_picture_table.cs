using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class create_picture_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannedImagePath",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ThumbnailImagePath",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BannerPictureId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltAttribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleAttribute = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BannerPictureId",
                table: "Blogs",
                column: "BannerPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ThumbnailId",
                table: "Blogs",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Pictures_BannerPictureId",
                table: "Blogs",
                column: "BannerPictureId",
                principalTable: "Pictures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Pictures_ThumbnailId",
                table: "Blogs",
                column: "ThumbnailId",
                principalTable: "Pictures",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Pictures_BannerPictureId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Pictures_ThumbnailId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BannerPictureId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ThumbnailId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BannerPictureId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "BannedImagePath",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImagePath",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
