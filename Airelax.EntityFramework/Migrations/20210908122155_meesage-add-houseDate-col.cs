using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class meesageaddhouseDatecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_HouseId",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_HouseId",
                table: "Messages",
                column: "HouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_HouseId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_HouseId",
                table: "Messages",
                column: "HouseId",
                unique: true);
        }
    }
}
