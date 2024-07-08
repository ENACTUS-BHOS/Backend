using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Presentation.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "Artists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Artists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Artists",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Artists");
        }
    }
}
