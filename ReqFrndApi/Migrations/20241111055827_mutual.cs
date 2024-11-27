using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReqFrndApi.Migrations
{
    /// <inheritdoc />
    public partial class mutual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendRequestId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MutualFriendId",
                table: "FriendRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FriendRequestId",
                table: "Users",
                column: "FriendRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FriendRequest_FriendRequestId",
                table: "Users",
                column: "FriendRequestId",
                principalTable: "FriendRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FriendRequest_FriendRequestId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FriendRequestId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FriendRequestId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MutualFriendId",
                table: "FriendRequest");
        }
    }
}
