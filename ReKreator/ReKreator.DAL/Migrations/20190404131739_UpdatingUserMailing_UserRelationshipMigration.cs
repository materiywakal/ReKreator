using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class UpdatingUserMailing_UserRelationshipMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMailing_User_UserMailingId",
                table: "UserMailing");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "UserMailing",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserMailing_UserId",
                table: "UserMailing",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMailing_User_UserId",
                table: "UserMailing",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMailing_User_UserId",
                table: "UserMailing");

            migrationBuilder.DropIndex(
                name: "IX_UserMailing_UserId",
                table: "UserMailing");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMailing");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMailing_User_UserMailingId",
                table: "UserMailing",
                column: "UserMailingId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
