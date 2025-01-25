using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBerryTypeBerryKindtoHarvestSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BerryKindId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BerryTypeId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BerryKindId",
                table: "Harvests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BerryTypeId",
                table: "Harvests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BerryKindId",
                table: "Sales",
                column: "BerryKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BerryTypeId",
                table: "Sales",
                column: "BerryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_BerryKindId",
                table: "Harvests",
                column: "BerryKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvests_BerryTypeId",
                table: "Harvests",
                column: "BerryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_BerryKinds_BerryKindId",
                table: "Harvests",
                column: "BerryKindId",
                principalTable: "BerryKinds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests",
                column: "BerryTypeId",
                principalTable: "BerryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_BerryKinds_BerryKindId",
                table: "Sales",
                column: "BerryKindId",
                principalTable: "BerryKinds",
                principalColumn: "Id");

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
                name: "FK_Harvests_BerryKinds_BerryKindId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Harvests_BerryTypes_BerryTypeId",
                table: "Harvests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_BerryKinds_BerryKindId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_BerryTypes_BerryTypeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_BerryKindId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_BerryTypeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Harvests_BerryKindId",
                table: "Harvests");

            migrationBuilder.DropIndex(
                name: "IX_Harvests_BerryTypeId",
                table: "Harvests");

            migrationBuilder.DropColumn(
                name: "BerryKindId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "BerryTypeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "BerryKindId",
                table: "Harvests");

            migrationBuilder.DropColumn(
                name: "BerryTypeId",
                table: "Harvests");
        }
    }
}
