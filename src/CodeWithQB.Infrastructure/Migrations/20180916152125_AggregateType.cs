using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class AggregateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AggregateDotNetType",
                table: "StoredEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregateDotNetType",
                table: "StoredEvents");
        }
    }
}
