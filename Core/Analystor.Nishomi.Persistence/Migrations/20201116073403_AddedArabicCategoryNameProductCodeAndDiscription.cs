using Microsoft.EntityFrameworkCore.Migrations;

namespace Analystor.Nishomi.Persistence.Migrations
{
    public partial class AddedArabicCategoryNameProductCodeAndDiscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCodeAr",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCodeAr",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Categories");
        }
    }
}
