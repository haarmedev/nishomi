using Microsoft.EntityFrameworkCore.Migrations;

namespace Analystor.Nishomi.Persistence.Migrations
{
    public partial class AdressUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CustomerRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "CustomerRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "CustomerRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "CustomerRequests");
        }
    }
}
