using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreUserPropertyAndUserExternalProviderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PhoneNumber",
                schema: "SaBooBo",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                schema: "SaBooBo",
                table: "User",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "SaBooBo",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFollowedMerchant",
                schema: "SaBooBo",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserExternalProvider",
                schema: "SaBooBo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserExternalId = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExternalProvider", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExternalProvider",
                schema: "SaBooBo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "SaBooBo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsFollowedMerchant",
                schema: "SaBooBo",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                schema: "SaBooBo",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                schema: "SaBooBo",
                table: "User",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
