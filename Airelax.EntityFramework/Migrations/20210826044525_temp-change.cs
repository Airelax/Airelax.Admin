using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class tempchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BedroomDetails",
                table: "BedroomDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BedroomDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 26, 12, 45, 24, 780, DateTimeKind.Local).AddTicks(5726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 21, 20, 53, 26, 751, DateTimeKind.Local).AddTicks(510));

            migrationBuilder.AddColumn<string>(
                name: "Temp",
                table: "BedroomDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BedroomDetails",
                table: "BedroomDetails",
                column: "Temp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BedroomDetails",
                table: "BedroomDetails");

            migrationBuilder.DropColumn(
                name: "Temp",
                table: "BedroomDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 21, 20, 53, 26, 751, DateTimeKind.Local).AddTicks(510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 26, 12, 45, 24, 780, DateTimeKind.Local).AddTicks(5726));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BedroomDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BedroomDetails",
                table: "BedroomDetails",
                column: "Id");
        }
    }
}
