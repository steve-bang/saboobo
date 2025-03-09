using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchantService.Migrations
{
    /// <inheritdoc />
    public partial class RenameMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaData",
                schema: "SaBooBo",
                table: "MerchantProviderSetting",
                newName: "Metadata");

            migrationBuilder.AlterColumn<string>(
                name: "Metadata",
                schema: "SaBooBo",
                table: "MerchantProviderSetting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Metadata",
                schema: "SaBooBo",
                table: "MerchantProviderSetting",
                newName: "MetaData");

            migrationBuilder.AlterColumn<string>(
                name: "MetaData",
                schema: "SaBooBo",
                table: "MerchantProviderSetting",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
