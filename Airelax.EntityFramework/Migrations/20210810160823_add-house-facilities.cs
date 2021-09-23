using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class addhousefacilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 11, 0, 8, 22, 915, DateTimeKind.Local).AddTicks(296),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 10, 12, 7, 44, 185, DateTimeKind.Local).AddTicks(262));

            migrationBuilder.AddColumn<string>(
                name: "NotProvideFacilities",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvideFacilities",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotProvideFacilities",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "ProvideFacilities",
                table: "Houses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 10, 12, 7, 44, 185, DateTimeKind.Local).AddTicks(262),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 11, 0, 8, 22, 915, DateTimeKind.Local).AddTicks(296));
        }
    }
}
