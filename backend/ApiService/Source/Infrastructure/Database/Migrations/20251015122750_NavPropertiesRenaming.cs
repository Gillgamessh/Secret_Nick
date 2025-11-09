using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class NavPropertiesRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gifts_GiftId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_GiftToUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GiftId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GiftToUserId",
                table: "Users",
                newName: "GiftRecipientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GiftToUserId",
                table: "Users",
                newName: "IX_Users_GiftRecipientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_GiftRecipientUserId",
                table: "Users",
                column: "GiftRecipientUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_GiftRecipientUserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GiftRecipientUserId",
                table: "Users",
                newName: "GiftToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GiftRecipientUserId",
                table: "Users",
                newName: "IX_Users_GiftToUserId");

            migrationBuilder.AddColumn<long>(
                name: "GiftId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GiftId",
                table: "Users",
                column: "GiftId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gifts_GiftId",
                table: "Users",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_GiftToUserId",
                table: "Users",
                column: "GiftToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
