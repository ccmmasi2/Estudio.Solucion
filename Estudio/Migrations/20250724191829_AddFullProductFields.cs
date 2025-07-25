using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estudio.Migrations
{
    /// <inheritdoc />
    public partial class AddFullProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FragranceType",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOutOfStock",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FragranceType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsOutOfStock",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Products",
                newName: "Category");
        }
    }
}
