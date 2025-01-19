using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersion : Migration
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
                defaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 89, DateTimeKind.Utc).AddTicks(2440),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 134, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Topping",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlImage",
                schema: "SaBooBo",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                schema: "SaBooBo",
                table: "Product",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 69, DateTimeKind.Utc).AddTicks(4240),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 114, DateTimeKind.Utc).AddTicks(4360))
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "SaBooBo",
                table: "Product",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "SaBooBo",
                table: "Product",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 134, DateTimeKind.Utc).AddTicks(9090),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 89, DateTimeKind.Utc).AddTicks(2440));

            migrationBuilder.AlterColumn<string>(
                name: "UrlImage",
                schema: "SaBooBo",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                schema: "SaBooBo",
                table: "Product",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "SaBooBo",
                table: "Product",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "SaBooBo",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 18, 14, 20, 54, 114, DateTimeKind.Utc).AddTicks(4360),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 1, 19, 5, 50, 26, 69, DateTimeKind.Utc).AddTicks(4240))
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "SaBooBo",
                table: "Product",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .OldAnnotation("Relational:ColumnOrder", 0);
        }
    }
}
