using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Index_on_DueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "DATEADD(DAY, 14, [BorrowDate])",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DueDate",
                table: "Orders",
                column: "DueDate")
                .Annotation("SqlServer:Include", new[] { "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_DueDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Orders");
        }
    }
}
