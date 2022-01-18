using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class fix_blog_comments_parent_comment_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Thumbsup",
                table: "Comments",
                newName: "ThumbsUp");

            migrationBuilder.RenameColumn(
                name: "Thumbsdown",
                table: "Comments",
                newName: "ThumbsDown");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ThumbsUp",
                table: "Comments",
                newName: "Thumbsup");

            migrationBuilder.RenameColumn(
                name: "ThumbsDown",
                table: "Comments",
                newName: "Thumbsdown");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
