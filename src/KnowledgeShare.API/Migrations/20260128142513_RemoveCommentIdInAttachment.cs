using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeShare.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommentIdInAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeBaseId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KnowledgeBaseId",
                table: "Attachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Attachments",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
