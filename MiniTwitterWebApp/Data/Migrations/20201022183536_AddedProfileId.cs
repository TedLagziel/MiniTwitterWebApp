using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniTwitterWebApp.Data.Migrations
{
    public partial class AddedProfileId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweet_Profile_ProfileId",
                table: "Tweet");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Tweet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tweet_Profile_ProfileId",
                table: "Tweet",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweet_Profile_ProfileId",
                table: "Tweet");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Tweet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tweet_Profile_ProfileId",
                table: "Tweet",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
