using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchantService.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "SaBooBo",
                table: "Merchant",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "SaBooBo",
                table: "Merchant");
        }
    }
}
