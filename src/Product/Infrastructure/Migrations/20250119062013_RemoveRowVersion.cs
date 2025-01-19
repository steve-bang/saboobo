using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Product");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Topping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 6, 20, 13, 341, DateTimeKind.Utc).AddTicks(3380),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 572, DateTimeKind.Utc).AddTicks(1290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 6, 20, 13, 309, DateTimeKind.Utc).AddTicks(9700),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 550, DateTimeKind.Utc).AddTicks(7920));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Topping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 572, DateTimeKind.Utc).AddTicks(1290),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 20, 13, 341, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Topping",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 550, DateTimeKind.Utc).AddTicks(7920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 20, 13, 309, DateTimeKind.Utc).AddTicks(9700));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Product",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
