using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Description_to_Rental_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BookRentals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BookRentals");
        }
    }
}
