using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AppUsers_AdminId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_AdminId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AdminId",
                table: "Chats",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AppUsers_AdminId",
                table: "Chats",
                column: "AdminId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
