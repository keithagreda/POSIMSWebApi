using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class addedRemarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RemarksId",
                table: "SalesHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeaders_RemarksId",
                table: "SalesHeaders",
                column: "RemarksId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesHeaders_Remarks_RemarksId",
                table: "SalesHeaders",
                column: "RemarksId",
                principalTable: "Remarks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesHeaders_Remarks_RemarksId",
                table: "SalesHeaders");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropIndex(
                name: "IX_SalesHeaders_RemarksId",
                table: "SalesHeaders");

            migrationBuilder.DropColumn(
                name: "RemarksId",
                table: "SalesHeaders");
        }
    }
}
