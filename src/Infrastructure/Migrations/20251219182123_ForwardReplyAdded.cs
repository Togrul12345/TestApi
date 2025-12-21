using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForwardReplyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageReaction_AppUsers_UserId",
                table: "MessageReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReaction_Messages_MessageId",
                table: "MessageReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageReaction",
                table: "MessageReaction");

            migrationBuilder.RenameTable(
                name: "MessageReaction",
                newName: "MessageReactions");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReaction_UserId",
                table: "MessageReactions",
                newName: "IX_MessageReactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReaction_MessageId",
                table: "MessageReactions",
                newName: "IX_MessageReactions_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageReactions",
                table: "MessageReactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReactions_AppUsers_UserId",
                table: "MessageReactions",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReactions_Messages_MessageId",
                table: "MessageReactions",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageReactions_AppUsers_UserId",
                table: "MessageReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReactions_Messages_MessageId",
                table: "MessageReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageReactions",
                table: "MessageReactions");

            migrationBuilder.RenameTable(
                name: "MessageReactions",
                newName: "MessageReaction");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReactions_UserId",
                table: "MessageReaction",
                newName: "IX_MessageReaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReactions_MessageId",
                table: "MessageReaction",
                newName: "IX_MessageReaction_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageReaction",
                table: "MessageReaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReaction_AppUsers_UserId",
                table: "MessageReaction",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReaction_Messages_MessageId",
                table: "MessageReaction",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
