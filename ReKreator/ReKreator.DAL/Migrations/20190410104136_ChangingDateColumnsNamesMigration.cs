using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class ChangingDateColumnsNamesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "User",
                newName: "RegisterOn");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Event",
                newName: "CreateOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterOn",
                table: "User",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Event",
                newName: "CreationDate");
        }
    }
}
