using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class addedInventorybeginningtosales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InventoryBeginningId",
                table: "SalesHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeaders_InventoryBeginningId",
                table: "SalesHeaders",
                column: "InventoryBeginningId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesHeaders_InventoryBeginnings_InventoryBeginningId",
                table: "SalesHeaders",
                column: "InventoryBeginningId",
                principalTable: "InventoryBeginnings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesHeaders_InventoryBeginnings_InventoryBeginningId",
                table: "SalesHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesHeaders_InventoryBeginningId",
                table: "SalesHeaders");

            migrationBuilder.DropColumn(
                name: "InventoryBeginningId",
                table: "SalesHeaders");
        }
    }
}
