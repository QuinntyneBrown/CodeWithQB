using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeWithQB.Infrastructure.Migrations
{
    public partial class AddMentoringSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talks_Locations_LocationId",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_Talks_LocationId",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Talks");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Products",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Talks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Talks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Talks_LocationId",
                table: "Talks",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Talks_Locations_LocationId",
                table: "Talks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
