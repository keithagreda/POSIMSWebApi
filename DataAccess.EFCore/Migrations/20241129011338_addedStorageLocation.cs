using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class addedStorageLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Customers_CustomerId",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_CustomerId",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesDetails");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SalesDetails",
                newName: "ProductPrice");

            migrationBuilder.AddColumn<int>(
                name: "StorageLocationId",
                table: "StocksHeaders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "SalesHeaders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ActualSellingPrice",
                table: "SalesDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalesDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "StorageLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StocksHeaders_StorageLocationId",
                table: "StocksHeaders",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeaders_CustomerId",
                table: "SalesHeaders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesHeaders_Customers_CustomerId",
                table: "SalesHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksHeaders_StorageLocation_StorageLocationId",
                table: "StocksHeaders",
                column: "StorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesHeaders_Customers_CustomerId",
                table: "SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksHeaders_StorageLocation_StorageLocationId",
                table: "StocksHeaders");

            migrationBuilder.DropTable(
                name: "StorageLocation");

            migrationBuilder.DropIndex(
                name: "IX_StocksHeaders_StorageLocationId",
                table: "StocksHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesHeaders_CustomerId",
                table: "SalesHeaders");

            migrationBuilder.DropColumn(
                name: "StorageLocationId",
                table: "StocksHeaders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesHeaders");

            migrationBuilder.DropColumn(
                name: "ActualSellingPrice",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesDetails");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "SalesDetails",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "SalesDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_CustomerId",
                table: "SalesDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Customers_CustomerId",
                table: "SalesDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
