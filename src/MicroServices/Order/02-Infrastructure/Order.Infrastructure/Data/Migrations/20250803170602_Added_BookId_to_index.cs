using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_BookId_to_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_DueDate",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DueDate",
                table: "Orders",
                column: "DueDate")
                .Annotation("SqlServer:Include", new[] { "BookId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_DueDate",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DueDate",
                table: "Orders",
                column: "DueDate")
                .Annotation("SqlServer:Include", new[] { "UserId" });
        }
    }
}
