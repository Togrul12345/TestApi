using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "SenderReceivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers",
                columns: new[] { "UserId", "ChatId", "ConnectionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "SenderReceivers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers",
                columns: new[] { "UserId", "ConnectionId" });
        }
    }
}
