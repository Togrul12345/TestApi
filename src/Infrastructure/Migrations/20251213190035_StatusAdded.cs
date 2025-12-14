using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageReplyId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reaction",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageReplyId",
                table: "Messages",
                column: "MessageReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageReplyId",
                table: "Messages",
                column: "MessageReplyId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageReplyId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageReplyId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageReplyId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Reaction",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Messages");
        }
    }
}
