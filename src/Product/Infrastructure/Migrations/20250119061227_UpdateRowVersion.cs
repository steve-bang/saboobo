using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRowVersion : Migration
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
                defaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 572, DateTimeKind.Utc).AddTicks(1290),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 89, DateTimeKind.Utc).AddTicks(2440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 550, DateTimeKind.Utc).AddTicks(7920),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 69, DateTimeKind.Utc).AddTicks(4240));
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
                defaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 89, DateTimeKind.Utc).AddTicks(2440),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 572, DateTimeKind.Utc).AddTicks(1290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 69, DateTimeKind.Utc).AddTicks(4240),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 6, 12, 27, 550, DateTimeKind.Utc).AddTicks(7920));
        }
    }
}
