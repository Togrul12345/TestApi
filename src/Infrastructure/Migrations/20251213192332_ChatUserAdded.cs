using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Chats_ChatId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ChatId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "AppUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ChatId",
                table: "AppUsers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Chats_ChatId",
                table: "AppUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
