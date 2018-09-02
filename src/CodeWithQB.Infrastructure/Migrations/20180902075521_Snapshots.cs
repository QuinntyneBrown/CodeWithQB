using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class Snapshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Snapshots",
                columns: table => new
                {
                    SnapshotId = table.Column<Guid>(nullable: false),
                    AsOfDateTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshots", x => x.SnapshotId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Snapshots");
        }
    }
}
