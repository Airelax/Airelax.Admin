using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class addcolhousereservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 10, 12, 7, 44, 185, DateTimeKind.Local).AddTicks(262),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 7, 23, 31, 54, 250, DateTimeKind.Local).AddTicks(6946));

            migrationBuilder.AddColumn<string>(
                name: "ReservationDates",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDates",
                table: "Houses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 7, 23, 31, 54, 250, DateTimeKind.Local).AddTicks(6946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 10, 12, 7, 44, 185, DateTimeKind.Local).AddTicks(262));
        }
    }
}
