using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class AddingIsNotifiedPropToEventHolding_UserEntityAndRenamingGenreToGenresAtEventEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Event_Genre",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Event");

            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "EventHolding_User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Genres",
                table: "Event",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Event_Genres",
                table: "Event",
                column: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Event_Genres",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "EventHolding_User");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Event");

            migrationBuilder.AddColumn<long>(
                name: "Genre",
                table: "Event",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Event_Genre",
                table: "Event",
                column: "Genre");
        }
    }
}
