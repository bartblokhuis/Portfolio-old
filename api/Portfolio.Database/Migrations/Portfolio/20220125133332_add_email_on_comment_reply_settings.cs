using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class add_email_on_comment_reply_settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOnCommentReplySubjectTemplate",
                table: "BlogSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailOnCommentReplyTemplate",
                table: "BlogSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendEmailOnCommentReply",
                table: "BlogSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOnCommentReplySubjectTemplate",
                table: "BlogSettings");

            migrationBuilder.DropColumn(
                name: "EmailOnCommentReplyTemplate",
                table: "BlogSettings");

            migrationBuilder.DropColumn(
                name: "IsSendEmailOnCommentReply",
                table: "BlogSettings");
        }
    }
}
