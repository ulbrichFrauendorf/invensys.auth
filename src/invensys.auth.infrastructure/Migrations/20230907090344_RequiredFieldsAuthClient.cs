using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invensys.auth.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequiredFieldsAuthClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthClients",
                table: "AuthClients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthClients");

            migrationBuilder.AddColumn<string>(
                name: "AuthClientId",
                table: "AuthClients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AllowedScopes",
                table: "AuthClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthClients",
                table: "AuthClients",
                column: "AuthClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthClients",
                table: "AuthClients");

            migrationBuilder.DropColumn(
                name: "AuthClientId",
                table: "AuthClients");

            migrationBuilder.DropColumn(
                name: "AllowedScopes",
                table: "AuthClients");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AuthClients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthClients",
                table: "AuthClients",
                column: "Id");
        }
    }
}
