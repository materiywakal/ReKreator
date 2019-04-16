using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class AddingIdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_User_Username",
                table: "User",
                newName: "IX_User_UserName");

            migrationBuilder.DropIndex(
                name: "IX_User_PasswordHash",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordHash",
                table: "User",
                column: "PasswordHash");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "User_UserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRole_RoleId",
                table: "User_UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_UserRole");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserName",
                table: "User",
                newName: "IX_User_Username");

            migrationBuilder.DropIndex(
                name: "IX_User_PasswordHash",
                table: "User");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordHash",
                table: "User",
                column: "PasswordHash");

            migrationBuilder.AddColumn<byte>(
                name: "Role",
                table: "User",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
