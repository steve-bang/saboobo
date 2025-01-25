using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressIsNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_EmailAddress",
                schema: "SaBooBo",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailAddress",
                schema: "SaBooBo",
                table: "User",
                column: "EmailAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_EmailAddress",
                schema: "SaBooBo",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailAddress",
                schema: "SaBooBo",
                table: "User",
                column: "EmailAddress",
                unique: true);
        }
    }
}
