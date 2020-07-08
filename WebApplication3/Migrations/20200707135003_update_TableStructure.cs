using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class update_TableStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Available_Quantity",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0);
         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {       
            migrationBuilder.DropColumn(
                name: "Available_Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Products");           
        }
    }
}
