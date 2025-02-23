using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class AddBShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "SaBooBo",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddress",
                schema: "SaBooBo",
                table: "ShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                schema: "SaBooBo",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "ShippingAddress",
                schema: "SaBooBo",
                newName: "Shipping_Address",
                newSchema: "SaBooBo");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                schema: "SaBooBo",
                newName: "OrderItem",
                newSchema: "SaBooBo");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                schema: "SaBooBo",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                schema: "SaBooBo",
                table: "OrderItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipping_Address",
                schema: "SaBooBo",
                table: "Shipping_Address",
                columns: new[] { "Id", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "SaBooBo",
                table: "OrderItem",
                columns: new[] { "Id", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_Address_OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "SaBooBo",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "SaBooBo",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_Address_Orders_OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address",
                column: "OrderId",
                principalSchema: "SaBooBo",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "SaBooBo",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_Address_Orders_OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipping_Address",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_Address_OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "SaBooBo",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "SaBooBo",
                table: "Shipping_Address");

            migrationBuilder.RenameTable(
                name: "Shipping_Address",
                schema: "SaBooBo",
                newName: "ShippingAddress",
                newSchema: "SaBooBo");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "SaBooBo",
                newName: "OrderItems",
                newSchema: "SaBooBo");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                schema: "SaBooBo",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "SaBooBo",
                table: "ShippingAddress",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "SaBooBo",
                table: "ShippingAddress",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "SaBooBo",
                table: "ShippingAddress",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "SaBooBo",
                table: "ShippingAddress",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "SaBooBo",
                table: "ShippingAddress",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                schema: "SaBooBo",
                table: "OrderItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddress",
                schema: "SaBooBo",
                table: "ShippingAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                schema: "SaBooBo",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "SaBooBo",
                table: "OrderItems",
                column: "OrderId",
                principalSchema: "SaBooBo",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
