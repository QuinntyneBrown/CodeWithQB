using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class SnapShotSequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Snapshots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Snapshots");
        }
    }
}
