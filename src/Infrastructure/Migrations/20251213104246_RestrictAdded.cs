using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestrictAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats",
                column: "SuperAdminId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AppUsers_SuperAdminId",
                table: "Chats",
                column: "SuperAdminId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
