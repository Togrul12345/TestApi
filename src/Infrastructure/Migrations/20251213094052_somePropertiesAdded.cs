using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class somePropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResolutionHeight",
                table: "Messages",
                newName: "VoiceMessage_Duration");

            migrationBuilder.RenameColumn(
                name: "DurationInSeconds",
                table: "Messages",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "AudioUrl",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VideoMessage_FileSize",
                table: "Messages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoiceMessage_ContentType",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VoiceMessage_FileSize",
                table: "Messages",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioUrl",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VideoMessage_FileSize",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VoiceMessage_ContentType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "VoiceMessage_FileSize",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "VoiceMessage_Duration",
                table: "Messages",
                newName: "ResolutionHeight");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Messages",
                newName: "DurationInSeconds");
        }
    }
}
