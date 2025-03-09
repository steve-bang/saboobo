using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                schema: "SaBooBo",
                table: "Shipping_Address",
                newName: "AddressDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressDetail",
                schema: "SaBooBo",
                table: "Shipping_Address",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
