using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MerchantId",
                schema: "SaBooBo",
                table: "User",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MerchantId",
                schema: "SaBooBo",
                table: "User");
        }
    }
}
