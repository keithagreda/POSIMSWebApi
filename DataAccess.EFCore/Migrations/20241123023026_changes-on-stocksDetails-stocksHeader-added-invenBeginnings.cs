using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class changesonstocksDetailsstocksHeaderaddedinvenBeginnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockDamageDetails_Products_ProductId",
                table: "StockDamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksReceivings_Products_ProductId",
                table: "StocksReceivings");

            migrationBuilder.DropIndex(
                name: "IX_StockDamageDetails_ProductId",
                table: "StockDamageDetails");

            migrationBuilder.DropColumn(
                name: "TransNum",
                table: "StockDamageHeaders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockDamageDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "StocksReceivings",
                newName: "StocksHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_StocksReceivings_ProductId",
                table: "StocksReceivings",
                newName: "IX_StocksReceivings_StocksHeaderId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StockDamageDetails",
                newName: "StockDetailId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StocksReceivings",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryBeginningId",
                table: "StocksReceivings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StockDamageHeaders",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "StockDamageHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StockDamageDetails",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesReturns",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockDetailId",
                table: "SalesReturns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesHeaders",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesDetails",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "Products",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysTillExpiration",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProdCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "ProductCategories",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "Customers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryBeginnings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeClosed = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
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
                    table.PrimaryKey("PK_InventoryBeginnings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StocksHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StocksHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StocksHeaders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StocksDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockNumInt = table.Column<int>(type: "int", nullable: false),
                    StocksHeaderId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StocksDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StocksDetails_StocksHeaders_StocksHeaderId",
                        column: x => x.StocksHeaderId,
                        principalTable: "StocksHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StocksReceivings_InventoryBeginningId",
                table: "StocksReceivings",
                column: "InventoryBeginningId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDamageDetails_StockDetailId",
                table: "StockDamageDetails",
                column: "StockDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_StockDetailId",
                table: "SalesReturns",
                column: "StockDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_StocksDetails_StocksHeaderId",
                table: "StocksDetails",
                column: "StocksHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StocksHeaders_ProductId",
                table: "StocksHeaders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_StocksDetails_StockDetailId",
                table: "SalesReturns",
                column: "StockDetailId",
                principalTable: "StocksDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockDamageDetails_StocksDetails_StockDetailId",
                table: "StockDamageDetails",
                column: "StockDetailId",
                principalTable: "StocksDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksReceivings_InventoryBeginnings_InventoryBeginningId",
                table: "StocksReceivings",
                column: "InventoryBeginningId",
                principalTable: "InventoryBeginnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksReceivings_StocksHeaders_StocksHeaderId",
                table: "StocksReceivings",
                column: "StocksHeaderId",
                principalTable: "StocksHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_StocksDetails_StockDetailId",
                table: "SalesReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_StockDamageDetails_StocksDetails_StockDetailId",
                table: "StockDamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksReceivings_InventoryBeginnings_InventoryBeginningId",
                table: "StocksReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksReceivings_StocksHeaders_StocksHeaderId",
                table: "StocksReceivings");

            migrationBuilder.DropTable(
                name: "InventoryBeginnings");

            migrationBuilder.DropTable(
                name: "StocksDetails");

            migrationBuilder.DropTable(
                name: "StocksHeaders");

            migrationBuilder.DropIndex(
                name: "IX_StocksReceivings_InventoryBeginningId",
                table: "StocksReceivings");

            migrationBuilder.DropIndex(
                name: "IX_StockDamageDetails_StockDetailId",
                table: "StockDamageDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_StockDetailId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "InventoryBeginningId",
                table: "StocksReceivings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "StockDamageHeaders");

            migrationBuilder.DropColumn(
                name: "StockDetailId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "DaysTillExpiration",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProdCode",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StocksHeaderId",
                table: "StocksReceivings",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_StocksReceivings_StocksHeaderId",
                table: "StocksReceivings",
                newName: "IX_StocksReceivings_ProductId");

            migrationBuilder.RenameColumn(
                name: "StockDetailId",
                table: "StockDamageDetails",
                newName: "Quantity");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StocksReceivings",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StockDamageHeaders",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<string>(
                name: "TransNum",
                table: "StockDamageHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "StockDamageDetails",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "StockDamageDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesReturns",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesHeaders",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "SalesDetails",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "Products",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "ProductCategories",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationTime",
                table: "Customers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.CreateIndex(
                name: "IX_StockDamageDetails_ProductId",
                table: "StockDamageDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockDamageDetails_Products_ProductId",
                table: "StockDamageDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksReceivings_Products_ProductId",
                table: "StocksReceivings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
