using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeBerryTypenullableinHarvestandSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_BerryTypes_BerryTypeId",
                table: "Sales");

            migrationBuilder.AlterColumn<Guid>(
                name: "BerryTypeId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BerryTypeId",
                table: "Harvests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests",
                column: "BerryTypeId",
                principalTable: "BerryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_BerryTypes_BerryTypeId",
                table: "Sales",
                column: "BerryTypeId",
                principalTable: "BerryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_BerryTypes_BerryTypeId",
                table: "Sales");

            migrationBuilder.AlterColumn<Guid>(
                name: "BerryTypeId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BerryTypeId",
                table: "Harvests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests",
                column: "BerryTypeId",
                principalTable: "BerryTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_BerryTypes_BerryTypeId",
                table: "Sales",
                column: "BerryTypeId",
                principalTable: "BerryTypes",
                principalColumn: "Id");
        }
    }
}
