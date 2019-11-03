using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication6.Migrations
{
    public partial class name3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name3",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name3",
                table: "Items");
        }
    }
}
