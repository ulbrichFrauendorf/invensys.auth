using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invensys.auth.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rework_User_Password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AuthUsers");
            
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AuthUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AuthUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AuthUsers");
            
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AuthUsers");
        }
    }
}
