using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class EditUser_UserRoleRelationshipMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole_UserRole_RoleId",
                table: "User_UserRole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User_UserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserRole_RoleId",
                table: "User_UserRole",
                newName: "IX_User_UserRole_UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole_UserRole_UserRoleId",
                table: "User_UserRole",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole_UserRole_UserRoleId",
                table: "User_UserRole");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "User_UserRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserRole_UserRoleId",
                table: "User_UserRole",
                newName: "IX_User_UserRole_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole_UserRole_RoleId",
                table: "User_UserRole",
                column: "RoleId",
                principalTable: "UserRole",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
