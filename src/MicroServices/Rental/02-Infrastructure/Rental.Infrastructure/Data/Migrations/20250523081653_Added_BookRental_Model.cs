using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_BookRental_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookRentals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExtended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRentals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RentalId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookRentalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalHistories_BookRentals_BookRentalId",
                        column: x => x.BookRentalId,
                        principalTable: "BookRentals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentalHistories_BookRentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "BookRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_BookRentalId",
                table: "RentalHistories",
                column: "BookRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_RentalId",
                table: "RentalHistories",
                column: "RentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalHistories");

            migrationBuilder.DropTable(
                name: "BookRentals");
        }
    }
}
