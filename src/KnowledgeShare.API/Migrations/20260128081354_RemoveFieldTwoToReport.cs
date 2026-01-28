using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeShare.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldTwoToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Reports",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
