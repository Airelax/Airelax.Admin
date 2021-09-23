using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class changebedroomdetailkeytoidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2021, 8, 26, 12, 45, 57, 63, DateTimeKind.Local).AddTicks(4446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 26, 12, 45, 24, 780, DateTimeKind.Local).AddTicks(5726));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BedroomDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BedroomDetails",
                table: "BedroomDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2021, 8, 26, 12, 45, 57, 63, DateTimeKind.Local).AddTicks(4446));

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
    }
}
