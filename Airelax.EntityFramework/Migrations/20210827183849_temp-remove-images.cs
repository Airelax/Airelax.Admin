using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class tempremoveimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 26, 12, 45, 57, 63, DateTimeKind.Local).AddTicks(4446));

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "HouseLocations",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "HouseLocations",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "WishLists",
                type: "image",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Photos",
                type: "image",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 26, 12, 45, 57, 63, DateTimeKind.Local).AddTicks(4446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Members",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "HouseLocations",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "HouseLocations",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
