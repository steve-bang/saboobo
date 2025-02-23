using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchantService.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantProviderSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantProviderSetting",
                schema: "SaBooBo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderType = table.Column<string>(type: "text", nullable: false),
                    MetaData = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantProviderSetting", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantProviderSetting",
                schema: "SaBooBo");
        }
    }
}
