using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultCreatedDateField : Migration
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
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 8, 30, 23, 719, DateTimeKind.Utc).AddTicks(3000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 8, 30, 23, 699, DateTimeKind.Utc).AddTicks(6260));
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
                defaultValue: new DateTime(2025, 1, 19, 8, 30, 23, 719, DateTimeKind.Utc).AddTicks(3000),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 8, 30, 23, 699, DateTimeKind.Utc).AddTicks(6260),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
