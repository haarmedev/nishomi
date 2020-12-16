using Microsoft.EntityFrameworkCore.Migrations;

namespace Analystor.Nishomi.Persistence.Migrations
{
    public partial class Order_Number_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "CustomerRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "CustomerRequests");
        }
    }
}
