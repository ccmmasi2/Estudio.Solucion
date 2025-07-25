using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estudio.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueProductIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandName_ProductName_FragranceType_Price_Gender",
                table: "Products",
                columns: new[] { "BrandName", "ProductName", "FragranceType", "Price", "Gender" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_BrandName_ProductName_FragranceType_Price_Gender",
                table: "Products");
        }
    }
}
