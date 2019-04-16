using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Genre = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    PosterUrl = table.Column<string>(maxLength: 1024, nullable: true),
                    SourceUrl = table.Column<string>(maxLength: 254, nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "EventPlace",
                columns: table => new
                {
                    EventPlaceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlace", x => x.EventPlaceId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 64, nullable: false),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    PasswordHash = table.Column<byte[]>(maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: true),
                    Role = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EventHolding",
                columns: table => new
                {
                    EventHoldingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<long>(nullable: false),
                    EventPlaceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHolding", x => x.EventHoldingId);
                    table.ForeignKey(
                        name: "FK_EventHolding_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventHolding_EventPlace_EventPlaceId",
                        column: x => x.EventPlaceId,
                        principalTable: "EventPlace",
                        principalColumn: "EventPlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMailing",
                columns: table => new
                {
                    UserMailingId = table.Column<long>(nullable: false),
                    MailingPeriod = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    LasTimeMailed = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMailing", x => x.UserMailingId);
                    table.ForeignKey(
                        name: "FK_UserMailing_User_UserMailingId",
                        column: x => x.UserMailingId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventHolding_User",
                columns: table => new
                {
                    EventHoldingId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    NotificationTimeBeforeEvent = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHolding_User", x => new { x.UserId, x.EventHoldingId });
                    table.ForeignKey(
                        name: "FK_EventHolding_User_EventHolding_EventHoldingId",
                        column: x => x.EventHoldingId,
                        principalTable: "EventHolding",
                        principalColumn: "EventHoldingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventHolding_User_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Genre",
                table: "Event",
                column: "Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Type",
                table: "Event",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_EventHolding_EventId",
                table: "EventHolding",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventHolding_EventPlaceId",
                table: "EventHolding",
                column: "EventPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventHolding_Time",
                table: "EventHolding",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_EventHolding_User_EventHoldingId",
                table: "EventHolding_User",
                column: "EventHoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPlace_Title",
                table: "EventPlace",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordHash",
                table: "User",
                column: "PasswordHash");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventHolding_User");

            migrationBuilder.DropTable(
                name: "UserMailing");

            migrationBuilder.DropTable(
                name: "EventHolding");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "EventPlace");
        }
    }
}
