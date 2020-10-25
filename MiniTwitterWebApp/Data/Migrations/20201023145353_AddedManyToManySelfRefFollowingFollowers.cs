using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniTwitterWebApp.Data.Migrations
{
    public partial class AddedManyToManySelfRefFollowingFollowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Profile_ProfileId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_ProfileId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Profile");

            migrationBuilder.CreateTable(
                name: "FollowersFollowing",
                columns: table => new
                {
                    FollowerId = table.Column<int>(nullable: false),
                    FollowingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowersFollowing", x => new { x.FollowerId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_FollowersFollowing_Profile_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowersFollowing_Profile_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowersFollowing_FollowingId",
                table: "FollowersFollowing",
                column: "FollowingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowersFollowing");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Profile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ProfileId",
                table: "Profile",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Profile_ProfileId",
                table: "Profile",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
