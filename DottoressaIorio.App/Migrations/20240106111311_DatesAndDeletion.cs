using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DottoressaIorio.App.Migrations
{
    public partial class DatesAndDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TherapyTemplates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TherapyTemplates",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "TherapyTemplates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Therapies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "Therapies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Patients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "Patients",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TherapyTemplates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TherapyTemplates");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "TherapyTemplates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Therapies");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "Therapies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "Patients");
        }
    }
}
