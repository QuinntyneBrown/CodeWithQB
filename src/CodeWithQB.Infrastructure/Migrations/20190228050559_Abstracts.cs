using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class Abstracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Courses");
        }
    }
}
