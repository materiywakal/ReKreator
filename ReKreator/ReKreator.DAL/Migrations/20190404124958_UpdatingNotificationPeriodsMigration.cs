using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class UpdatingNotificationPeriodsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "EventHolding_User");

            migrationBuilder.DropColumn(
                name: "NotificationTimeBeforeEvent",
                table: "EventHolding_User");

            migrationBuilder.AddColumn<int>(
                name: "NotificationPeriodsBeforeEvent",
                table: "EventHolding_User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationPeriodsBeforeEvent",
                table: "EventHolding_User");

            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "EventHolding_User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationTimeBeforeEvent",
                table: "EventHolding_User",
                nullable: true);
        }
    }
}
