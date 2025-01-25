using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBerryTypeBerryKindtableswithonetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BerryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BerryKinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BerryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerryKinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BerryKinds_BerryTypes_BerryTypeId",
                        column: x => x.BerryTypeId,
                        principalTable: "BerryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BerryKinds_BerryTypeId",
                table: "BerryKinds",
                column: "BerryTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BerryKinds");

            migrationBuilder.DropTable(
                name: "BerryTypes");
        }
    }
}
