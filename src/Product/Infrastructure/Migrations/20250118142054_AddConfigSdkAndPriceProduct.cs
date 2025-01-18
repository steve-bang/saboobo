using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigSdkAndPriceProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Topping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 134, DateTimeKind.Utc).AddTicks(9090),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 34, 56, 16, DateTimeKind.Utc).AddTicks(5310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 114, DateTimeKind.Utc).AddTicks(4360),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 17, 14, 34, 56, 4, DateTimeKind.Utc).AddTicks(4130));

            migrationBuilder.AddColumn<long>(
                name: "Price",
                schema: "SaBooBo",
                table: "Product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                schema: "SaBooBo",
                table: "Product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "SaBooBo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Sku",
                schema: "SaBooBo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                schema: "SaBooBo",
                table: "Product");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Topping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 34, 56, 16, DateTimeKind.Utc).AddTicks(5310),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 134, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 17, 14, 34, 56, 4, DateTimeKind.Utc).AddTicks(4130),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 114, DateTimeKind.Utc).AddTicks(4360));
        }
    }
}
