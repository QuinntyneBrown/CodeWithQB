using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoredEvents",
                columns: table => new
                {
                    StoredEventId = table.Column<Guid>(nullable: false),
                    StreamId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Aggregate = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    DotNetType = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvents", x => x.StoredEventId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredEvents");
        }
    }
}
