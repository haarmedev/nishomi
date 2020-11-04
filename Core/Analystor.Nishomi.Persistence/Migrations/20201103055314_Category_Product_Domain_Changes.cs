using Microsoft.EntityFrameworkCore.Migrations;

namespace Analystor.Nishomi.Persistence.Migrations
{
    public partial class Category_Product_Domain_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainImage",
                table: "ProductImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainImage",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");
        }
    }
}
