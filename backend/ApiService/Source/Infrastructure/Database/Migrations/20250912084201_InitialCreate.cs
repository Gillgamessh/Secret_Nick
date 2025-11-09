using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    InfoLink = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdminId = table.Column<long>(type: "bigint", nullable: true),
                    InvitationCode = table.Column<string>(type: "text", nullable: false),
                    MinUsersLimit = table.Column<long>(type: "bigint", nullable: false, defaultValue: 3L),
                    MaxUsersLimit = table.Column<long>(type: "bigint", nullable: false, defaultValue: 20L),
                    MaxWishesLimit = table.Column<long>(type: "bigint", nullable: false, defaultValue: 5L),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    InvitationNote = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    GiftExchangeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GiftMaximumBudget = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    AuthCode = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DeliveryInfo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    GiftToUserId = table.Column<long>(type: "bigint", nullable: true),
                    GiftId = table.Column<long>(type: "bigint", nullable: true),
                    WantSurprise = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Interests = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_GiftToUserId",
                        column: x => x.GiftToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_UserId",
                table: "Gifts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AdminId",
                table: "Rooms",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_InvitationCode",
                table: "Rooms",
                column: "InvitationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthCode",
                table: "Users",
                column: "AuthCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GiftId",
                table: "Users",
                column: "GiftId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GiftToUserId",
                table: "Users",
                column: "GiftToUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomId",
                table: "Users",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Users_UserId",
                table: "Gifts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Users_UserId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AdminId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
