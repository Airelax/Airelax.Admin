using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class addmembercol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 21, 20, 53, 26, 751, DateTimeKind.Local).AddTicks(510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 18, 15, 52, 23, 270, DateTimeKind.Local).AddTicks(3956));

            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Members",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkTime",
                table: "MemberInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "WorkTime",
                table: "MemberInfos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterTime",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 18, 15, 52, 23, 270, DateTimeKind.Local).AddTicks(3956),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 21, 20, 53, 26, 751, DateTimeKind.Local).AddTicks(510));
        }
    }
}
