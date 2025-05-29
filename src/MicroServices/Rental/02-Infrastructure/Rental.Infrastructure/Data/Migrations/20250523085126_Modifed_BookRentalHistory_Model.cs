using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Modifed_BookRentalHistory_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHistories_BookRentals_BookRentalId",
                table: "RentalHistories");

            migrationBuilder.DropIndex(
                name: "IX_RentalHistories_BookRentalId",
                table: "RentalHistories");

            migrationBuilder.DropColumn(
                name: "BookRentalId",
                table: "RentalHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookRentalId",
                table: "RentalHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_BookRentalId",
                table: "RentalHistories",
                column: "BookRentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHistories_BookRentals_BookRentalId",
                table: "RentalHistories",
                column: "BookRentalId",
                principalTable: "BookRentals",
                principalColumn: "Id");
        }
    }
}
