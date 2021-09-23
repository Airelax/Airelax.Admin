using Microsoft.EntityFrameworkCore.Migrations;

namespace Airelax.EntityFramework.Migrations
{
    public partial class meesageaddhouseIdcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HouseId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_HouseId",
                table: "Messages",
                column: "HouseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Houses_HouseId",
                table: "Messages",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Houses_HouseId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_HouseId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Messages");
        }
    }
}
