﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReKreator.DAL.Migrations
{
    public partial class AddingRegistrationDateToUserEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "User",
                nullable: false,
                defaultValue: DateTime.Today);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "User");
        }
    }
}
