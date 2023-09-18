using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invensys.auth.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAudience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AuthClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "AuthClients");
        }
    }
}
