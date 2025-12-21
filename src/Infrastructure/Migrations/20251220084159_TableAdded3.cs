using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableAdded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "SenderReceivers");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "SenderReceivers",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers",
                columns: new[] { "UserId", "ConnectionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SenderReceivers",
                newName: "ReceiverId");

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "SenderReceivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderReceivers",
                table: "SenderReceivers",
                columns: new[] { "SenderId", "ReceiverId", "ConnectionId" });
        }
    }
}
