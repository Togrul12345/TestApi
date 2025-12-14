using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminAddedAsAProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuperAdminId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AdminId",
                table: "Chats",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SuperAdminId",
                table: "Chats",
                column: "SuperAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AppUsers_AdminId",
                table: "Chats",
                column: "AdminId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats",
                column: "SuperAdminId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AppUsers_AdminId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_AdminId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SuperAdminId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SuperAdminId",
                table: "Chats");
        }
    }
}
